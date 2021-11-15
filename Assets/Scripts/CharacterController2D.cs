using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class CharacterController2D : MonoBehaviour
    {
        [SerializeField]
        private float _jumpForce = 400f;

        [SerializeField]
        private float _wallJumpForce = 20f;
        [Range(0, 1f)]
        [SerializeField] private float _acceleration = .05f;
        [Range(0, 1f)]
        [SerializeField] private float _inAirAcceleration = .05f;
        [SerializeField] private bool _airControl = false;
        [SerializeField] private LayerMask _groundLayers;

        [SerializeField] private BoxCollider2D _groundCheck;
        [SerializeField] private BoxCollider2D _wallCheck;
        [SerializeField] private BoxCollider2D _wallSlideCheck;
        [SerializeField] private float _allowedWallSlideTime = 4f;

        private Rigidbody2D _rigidbody2D;
        private bool _facingRight = true;
        private float _limitFallSpeed = 25f;

        public bool IsGrounded { get { return _groundCheck.IsTouchingLayers(_groundLayers); } }
        public bool IsOnWall { get { return !IsGrounded && _wallCheck.IsTouchingLayers(_groundLayers); } }
        public bool WallSlideCheck { get { return _wallSlideCheck.IsTouchingLayers(_groundLayers); } }
        public bool IsWallSliding { get; private set; } = false;

        private bool _canWallSlide = true;
        private float _towardsWall;

        private CancellationTokenSource tokenSource;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(float move, bool jump)
        {
            var isGrounded = IsGrounded;

            // Regular movement
            if (isGrounded || _airControl)
            {
                if (isGrounded)
                    _canWallSlide = true;

                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2D.velocity.y);

                var accel = isGrounded ? _acceleration : _inAirAcceleration;
                _rigidbody2D.velocity = Vector3.Lerp(_rigidbody2D.velocity, targetVelocity, accel);
            }

            // Jump
            if (jump)
            {
                if (isGrounded)
                {
                    // Add a vertical force to the player.
                    isGrounded = false;
                    _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
                }
                else if (IsWallSliding)
                {
                    CancelWallSlide();
                    _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
                    _rigidbody2D.velocity = new Vector2(-_towardsWall, 0);
                }
            }

            if (IsWallSliding)
            {

                if (_rigidbody2D.velocity.x < .1f)
                {
                    _rigidbody2D.velocity = new Vector3(_towardsWall, 0, 0);
                }
                else
                {
                    _rigidbody2D.velocity = new Vector3(_rigidbody2D.velocity.x, 0, 0);
                }

                if (!WallSlideCheck)
                {
                    CancelWallSlide();
                }
            }
            else if (!isGrounded && IsOnWall)
            {
                if (_canWallSlide)
                {
                    tokenSource = new CancellationTokenSource();
                    _towardsWall = _rigidbody2D.velocity.x * _wallJumpForce;
                    Flip(-_rigidbody2D.velocity.x);
                    IsWallSliding = true;
                    Task.Run(() => WallSlideCooldown(tokenSource.Token), tokenSource.Token);
                }
                else
                {
                    _rigidbody2D.velocity = new Vector3(0, _rigidbody2D.velocity.y, 0);
                }
            }


            // Limit fall speed
            if (_rigidbody2D.velocity.y < -_limitFallSpeed)
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_limitFallSpeed);

            // Fix orientation
            Flip(_rigidbody2D.velocity.x);
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
            tokenSource.Cancel();
        }

        private async Task WallSlideCooldown(CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(_allowedWallSlideTime));
            if (token.IsCancellationRequested)
                return;
            CancelWallSlide();
            _canWallSlide = false;
        }
    }
}
