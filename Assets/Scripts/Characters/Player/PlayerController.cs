using System;
using System.Threading;
using System.Threading.Tasks;
using ScriptableObjects;
using ScriptableObjects.Stats;
using Spine.Unity;
using UnityEngine;

namespace Characters.Player
{
    public enum PlayerState
    {
        Running,
        Idle
    }

    public class PlayerController : MonoBehaviour
    {
        private CharacterController2D _controller;
        [SerializeField] private SkeletonAnimation _characterCell = null;
        private PlayerState _playerState = PlayerState.Idle;

        [Header("Player Properties")]
        [SerializeField] private PlayerStats _stats;

        private Vector2 _moveDirection = Vector2.zero;

        private float _horizontalMove = 0f;
        private bool _jump = false;

        private CancellationTokenSource _playerHitToken = new CancellationTokenSource();
        private bool _hitEffect = false;
        private CancellationTokenSource _enemyKilledToken = new CancellationTokenSource();
        private bool _bounceEffect = false;

        private void OnEnable()
        {
            _controller = GetComponent<CharacterController2D>();

            _stats.InputHandler.JumpEvent += OnJump;
            _stats.InputHandler.LeftRightEvent += OnLeftRight;
            _playerState = PlayerState.Idle;

            _stats.KilledEnemyEvent += OnEnemyKilled;
            _stats.PlayerHitEvent += OnPlayerHit;
        }

        private void OnDisable()
        {
            _stats.InputHandler.JumpEvent -= OnJump;
            _stats.InputHandler.LeftRightEvent -= OnLeftRight;

            _stats.KilledEnemyEvent -= OnEnemyKilled;
            _stats.PlayerHitEvent -= OnPlayerHit;
        }

        private void FixedUpdate()
        {
            if (_hitEffect || GameManager.Instance.State != GameState.Playing)
                return;

            _horizontalMove = _moveDirection.x * _stats.MovementSpeed;

            // Move our character
            _controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump);

            if (!_controller.IsWallSliding && Math.Abs(_controller.Velocity.x) > 0.2f)
            {
                if (_playerState != PlayerState.Running)
                {
                    _playerState = PlayerState.Running;
                    _characterCell.AnimationState.SetAnimation(0, "run", true);
                }
            }
            else
            {
                if (_playerState != PlayerState.Idle)
                {
                    _playerState = PlayerState.Idle;
                    _characterCell.AnimationState.SetAnimation(0, "idle", true);
                }
            }

            _jump = false;
        }

        private void OnLeftRight(float val)
        {
            _moveDirection.x = val;
        }

        private void OnJump(bool pressed)
        {
            _jump = pressed;
        }

        private void OnPlayerHit(float direction)
        {
            if (_hitEffect)
                return;

            _playerHitToken.Cancel();
            _playerHitToken = new CancellationTokenSource();
            _hitEffect = true;
            Task.Run(() => ResetHitEffect(_playerHitToken.Token));
            _controller.JumpAway(-direction * (_stats.IsAlive ? 25 : 5));
        }

        private async Task ResetHitEffect(CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(.1), token);
            if (token.IsCancellationRequested)
                return;
            _hitEffect = false;
        }

        private void OnEnemyKilled()
        {
            if (_bounceEffect)
                return;

            _enemyKilledToken.Cancel();
            _bounceEffect = true;
            _enemyKilledToken = new CancellationTokenSource();
            Task.Run(() => ResetKilledEffect(_enemyKilledToken.Token));
            _controller.Bounce();
        }

        private async Task ResetKilledEffect(CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(.1), token);
            if (token.IsCancellationRequested)
                return;
            _bounceEffect = false;
        }
    }
}
