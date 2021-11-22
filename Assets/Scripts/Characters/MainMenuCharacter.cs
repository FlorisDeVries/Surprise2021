using System;
using System.Threading;
using System.Threading.Tasks;
using Spine.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters
{
    public enum WanderState
    {
        StartIdle,
        Idle,
        StartWander,
        Wander
    }

    public class MainMenuCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterController2D _controller;
        [SerializeField] private Transform _minBoundary;
        [SerializeField] private Transform _maxBoundary;

        [SerializeField] [Range(0, 20)] private float _idleTimeRange;
        [SerializeField] [Range(0, 5)] private float _wanderTimeRange;

        [SerializeField] private float _speed = 1;

        [SerializeField] private SkeletonAnimation _characterCell = null;
        private WanderState _state = WanderState.Idle;
        private float _direction = -1;

        private CancellationTokenSource _tokenSource;
        
        private void OnEnable()
        {
            _state = WanderState.StartIdle;
            _tokenSource = new CancellationTokenSource();
        }

        private void Update()
        {
            switch (_state)
            {
                case WanderState.StartIdle:
                    _direction = 0;

                    _characterCell.AnimationState.SetAnimation(0, "idle", true);
                    _state = WanderState.Idle;

                    var idleDelay = Random.Range(2, _idleTimeRange);
                    Task.Run(() => SetStateDelay(idleDelay, WanderState.StartWander, _tokenSource.Token), _tokenSource.Token);
                    break;
                case WanderState.Idle:
                    _controller.Move(0, false);
                    break;
                case WanderState.StartWander:
                    if (transform.position.x > _maxBoundary.position.x)
                    {
                        _direction = -1;
                    }
                    else if (transform.position.x < _minBoundary.position.x)
                    {
                        _direction = 1;
                    }
                    else
                    {
                        _direction = Random.Range(0f, 1f) > .5f ? -1 : 1;
                    }
                    
                    _characterCell.AnimationState.SetAnimation(0, "walk", true);
                    _state = WanderState.Wander;

                    var wanderDelay = Random.Range(.5f, _wanderTimeRange);
                    Task.Run(() => SetStateDelay(wanderDelay, WanderState.StartIdle, _tokenSource.Token), _tokenSource.Token);
                    break;
                case WanderState.Wander:
                    var direction = _direction * _speed;
                    _controller.Move(direction, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task SetStateDelay(float delay, WanderState state, CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay), token);
            _state = state;
        }
    }
}