using System;
using Assets.Scripts.ScriptableObjects;
using Spine.Unity;
using UnityEngine;

namespace Assets.Scripts.Player
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

        [Header("ScriptableObjects")]
        [SerializeField] private InputHandler _input;

        [Header("Player Properties")]
        [SerializeField] private float _speed = 350;

        private Vector2 _moveDirection = Vector2.zero;

        private float _horizontalMove = 0f;
        private bool _jump = false;
        private bool _dash = false;

        private void OnEnable()
        {
            _controller = GetComponent<CharacterController2D>();

            _input.JumpEvent += OnJump;
            _input.LeftRightEvent += OnLeftRight;
            _playerState = PlayerState.Idle;
        }

        private void OnDisable()
        {
            _input.JumpEvent -= OnJump;
            _input.LeftRightEvent -= OnLeftRight;
        }

        private void FixedUpdate()
        {
            _horizontalMove = _moveDirection.x * _speed;

            // Move our character
            _controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump);

            if (!_controller.IsWallSliding && Math.Abs(_horizontalMove) > 0)
            {
                if(_playerState != PlayerState.Running)
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
            _dash = false;
        }

        private void OnLeftRight(float val)
        {
            _moveDirection.x = val;
        }

        private void OnJump(bool pressed)
        {
            _jump = pressed;
        }
    }
}
