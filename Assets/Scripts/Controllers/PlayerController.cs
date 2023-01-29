using UnityEngine;
using UnityEngine.InputSystem;

namespace PF.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 1;

        private Vector2 _moveInput;
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
        
        private bool MovePlayer(Vector2 direction)
        {
            var moveVector = direction * (moveSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + moveVector);
            return true;
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