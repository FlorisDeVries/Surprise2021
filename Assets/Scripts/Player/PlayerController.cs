using System;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Animator _animator;
        private CharacterController2D _controller;

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
