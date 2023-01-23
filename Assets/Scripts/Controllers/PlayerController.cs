using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PF.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 1f;
        public float collisionOffset = 0.05f;
        public ContactFilter2D movementFilter;

        private Vector2 _moveInput;
        private readonly List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
        private Rigidbody2D _rb;
        private Animator _animator;

        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");
        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        public void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void FixedUpdate()
        {

            // rb.MovePosition(rb.position + (moveInput * moveSpeed * Time.fixedDeltaTime));

            if (_moveInput != Vector2.zero)
            {
                var success = MovePlayer(_moveInput);

                if (!success)
                {
                    success = MovePlayer(new Vector2(_moveInput.x, 0));
                    if (!success)
                    {
                        success = MovePlayer(new Vector2(0, _moveInput.y));
                    }
                }

                _animator.SetBool(IsMoving, success);
            }
            else
            {
                _animator.SetBool(IsMoving, false);
            }


        }

        // Tries to move the player in a direction by casting in that direction by the amount
        // moved plus an offset. If no collisions are found, it moves the players
        // Returns true or false depending on if a move was executed
        private bool MovePlayer(Vector2 direction)
        {
            // Check for potential collisions
            int count = _rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                _castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime +
                collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                var moveVector = direction * (moveSpeed * Time.fixedDeltaTime);
                _rb.MovePosition(_rb.position + moveVector);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
            if (_moveInput == Vector2.zero) return;
            _animator.SetFloat(InputX, _moveInput.x);
            _animator.SetFloat(InputY, _moveInput.y);
        }
    }
}