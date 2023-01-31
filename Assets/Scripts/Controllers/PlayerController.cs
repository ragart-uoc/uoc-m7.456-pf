using UnityEngine;
using UnityEngine.InputSystem;

namespace PF.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        /// <value>Property <c>moveSpeed</c> represents the player's movement speed.</value>
        private float _moveSpeed;

        /// <value>Property <c>_moveInput</c> represents the player's movement input.</value>
        private Vector2 _moveInput;
        
        /// <value>Property <c>_rb</c> represents the player's rigidbody.</value>
        private Rigidbody2D _rb;
        
        /// <value>Property <c>_animator</c> represents the player's animator.</value>
        private Animator _animator;

        /// <value>Property <c>InputX</c> represents the player's input on the x-axis.</value>
        private static readonly int InputX = Animator.StringToHash("InputX");
        
        /// <value>Property <c>InputY</c> represents the player's input on the y-axis.</value>
        private static readonly int InputY = Animator.StringToHash("InputY");
        
        /// <value>Property <c>IsMoving</c> represents the player's movement.</value>
        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        public void Awake()
        {
            // Get movement speed from the player preferences
            _moveSpeed = PlayerPrefs.GetFloat("moveSpeed", 1f);
        }
        
        /// <summary>
        /// Method <c>Start</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        public void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Method <c>FixedUpdate</c> is called every fixed frame-rate frame, if the MonoBehaviour is enabled.
        /// </summary>
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
        
        /// <summary>
        /// Method <c>MovePlayer</c> moves the player.
        /// </summary>
        private bool MovePlayer(Vector2 direction)
        {
            var moveVector = direction * (_moveSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + moveVector);
            return true;
        }

        /// <summary>
        /// Method <c>OnMove</c> is called when the player moves.
        /// </summary>
        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
            if (_moveInput == Vector2.zero) return;
            _animator.SetFloat(InputX, _moveInput.x);
            _animator.SetFloat(InputY, _moveInput.y);
        }
    }
}