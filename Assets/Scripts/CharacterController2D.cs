using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class CharacterController2D : MonoBehaviour
    {
        [SerializeField]
        private float _jumpForce = 400f;
        [Range(0, .3f)]
        [SerializeField] private float _movementSmoothing = .05f;
        [SerializeField] private bool _airControl = false;
        [SerializeField] private LayerMask _groundLayers;
        [SerializeField] private BoxCollider2D _groundCheck;
        [SerializeField] private BoxCollider2D _wallCheck;

        private bool _isGrounded = false;
        private Rigidbody2D _rigidbody2D;
        private bool _facingRight = true;
        private Vector3 _velocity = Vector3.zero;
        private float _limitFallSpeed = 25f;

        [SerializeField] private bool _isWall = false;
        private bool _isWallSliding = false;

        private bool _canMove = true; //If player can move

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(float move, bool jump)
        {
            if (!_canMove)
                return;

            CheckGrounded();
            CheckWall();

            // Regular movement
            if (_isGrounded || _airControl)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2D.velocity.y);
                _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);
            }

            // Jump
            if (jump)
            {
                if (_isGrounded)
                {
                    // Add a vertical force to the player.
                    _isGrounded = false;
                    _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
                }
                else if (!_isGrounded && _isWall)
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
                    _rigidbody2D.AddForce(new Vector2(_facingRight ? -4f * _jumpForce : 4f * _jumpForce, _jumpForce / 1.2f));
                }
            }

            // Prevent getting stuck on walls
            if (!_isGrounded && _isWall)
            {
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            }

            // Limit fall speed
            if (_rigidbody2D.velocity.y < -_limitFallSpeed)
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_limitFallSpeed);

            // Fix orientation
            Flip(_rigidbody2D.velocity.x);
        }

        private void CheckGrounded()
        {
            _isGrounded = _groundCheck.IsTouchingLayers(_groundLayers);
        }

        private void CheckWall()
        {
            _isWall = _wallCheck.IsTouchingLayers(_groundLayers);
        }

        private void Flip(float move)
        {
            // If the input is moving the player right and the player is facing left...
            if ((move > 0 && !_facingRight && !_isWallSliding) || (move < 0 && _facingRight && !_isWallSliding))
            {
                // Switch the way the player is labelled as facing.
                _facingRight = !_facingRight;

                // Multiply the player's x local scale by -1.
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
    }
}
