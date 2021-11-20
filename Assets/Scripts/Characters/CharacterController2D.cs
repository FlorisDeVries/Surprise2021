using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters
{
    public class CharacterController2D : MonoBehaviour
    {
        [Range(0, 1f)]
        [SerializeField] private float _acceleration = .05f;
        [Range(0, 1f)]
        [SerializeField] private float _inAirAcceleration = .05f;

        [SerializeField] private float _jumpForce = 400f;
        [SerializeField] private float _wallJumpForce = 20f;
        [SerializeField] private bool _airControl = false;

        [SerializeField] private BoxCollider2D _groundCheck;
        [SerializeField] private BoxCollider2D _wallCheck;
        [SerializeField] private BoxCollider2D _wallSlideCheck;
        [SerializeField] private LayerMask _groundLayers;

        [SerializeField] private float _allowedWallSlideTime = 4f;

        private Rigidbody2D _rigidbody2D;
        private bool _facingRight = true;
        [SerializeField] private float _limitFallSpeed = 25f;

        private bool IsGrounded => _groundCheck.IsTouchingLayers(_groundLayers);
        private bool IsOnWall => !IsGrounded && _wallCheck.IsTouchingLayers(_groundLayers);
        private bool WallSlideCheck => _wallSlideCheck.IsTouchingLayers(_groundLayers);
        
        public bool IsWallSliding { get; private set; } = false;
        public Vector2 Velocity => _rigidbody2D.velocity;

        private bool _canWallSlide = true;
        private float _towardsWall;

        private CancellationTokenSource _tokenSource;

        // State booleans
        private bool _isGrounded = true;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(float move, bool jump)
        {
            _isGrounded = IsGrounded;

            // Regular movement
            if (_isGrounded || _airControl)
                GroundMove(move);
            WallSlideLogic();
            JumpLogic(jump);

            // Limit fall speed
            _rigidbody2D.gravityScale = _rigidbody2D.velocity.y < 0 ? 4 : 2;

            if (_rigidbody2D.velocity.y < -_limitFallSpeed)
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_limitFallSpeed);

            // Fix orientation
            Flip(_rigidbody2D.velocity.x);
        }

        private void GroundMove(float move)
        {
            var acceleration = _inAirAcceleration;

            if (_isGrounded)
            {
                _canWallSlide = true;
                acceleration = _acceleration;
            }
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2D.velocity.y);

            _rigidbody2D.velocity = Vector3.Lerp(_rigidbody2D.velocity, targetVelocity, acceleration);
        }

        private void WallSlideLogic()
        {
            if (IsWallSliding)
            {
                // Stick player to wall
                _rigidbody2D.velocity = _rigidbody2D.velocity.x < .2f ? new Vector3(_towardsWall * 100, 0, 0) : new Vector3(_rigidbody2D.velocity.x, 0, 0);

                if (!WallSlideCheck)
                {
                    CancelWallSlide();
                }
            }
            else if (!_isGrounded && IsOnWall)
            {
                if (_canWallSlide)
                {
                    _tokenSource = new CancellationTokenSource();
                    _towardsWall = (_facingRight ? 1 : -1) * _wallJumpForce;
                    Flip(-_rigidbody2D.velocity.x);
                    IsWallSliding = true;
                    Task.Run(() => WallSlideCooldown(_tokenSource.Token), _tokenSource.Token);
                }
                else
                {
                    _rigidbody2D.velocity = new Vector3(0, _rigidbody2D.velocity.y, 0);
                }
            }
        }

        private void JumpLogic(bool jump)
        {
            // Jump
            if (!jump)
                return;

            if (_isGrounded)
            {
                // Add a vertical force to the player.
                _isGrounded = false;
                _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
            }
            else if (IsWallSliding)
            {
                CancelWallSlide();
                JumpAway(-_towardsWall);
            }
        }

        public void JumpAway(float direction)
        {
            _rigidbody2D.velocity = new Vector2(direction, 0);
            _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
        }

        public void Bounce()
        {
            _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
        }

        private void Flip(float move)
        {
            // If the input is moving the player right and the player is facing left...
            if ((move > 0 && !_facingRight && !IsWallSliding) || (move < 0 && _facingRight && !IsWallSliding))
            {
                // Switch the way the player is labelled as facing.
                _facingRight = !_facingRight;

                // Multiply the player's x local scale by -1.
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }

        private void CancelWallSlide()
        {
            IsWallSliding = false;
            _tokenSource.Cancel();
        }

        private async Task WallSlideCooldown(CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(_allowedWallSlideTime), token);
            if (token.IsCancellationRequested)
                return;
            CancelWallSlide();
            _canWallSlide = false;
        }
    }
}
