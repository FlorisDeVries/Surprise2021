using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ScriptableObjects.Stats;
using UnityEngine;

namespace Characters
{
    public class GoombaEnemy : MonoBehaviour
    {
        [Header("Player Properties")]
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private BoxCollider2D _collider;

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _deadSprite;

        [Header("Pathfinding")]
        [SerializeField] private List<Transform> _targets;
        [SerializeField] private float _speed = 10;
        private int _targetIndex = 0;

        public string isDyingStatus = "";
        private Vector3 _tagetScale = Vector3.one;
        private bool _handlingCollision = false;
        private bool _dead = false;
        private bool _hitCooldown = false;

        private CancellationTokenSource deathAnimToken = new CancellationTokenSource();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_hitCooldown || collision.gameObject.tag != "Player" || _handlingCollision || _dead)
                return;

            _hitCooldown = true;
            _handlingCollision = true;

            var contact = collision.GetContact(0);
            if (Mathf.Abs(contact.normal.y) > .5f)
            {
                // Die
                _collider.enabled = false;
                _dead = true;
                _playerStats.KilledEnemy();
                _renderer.sprite = _deadSprite;

                // Dying
                isDyingStatus = "Dying";
                deathAnimToken = new CancellationTokenSource();
                Task.Run(async () => await DeathAnim(deathAnimToken.Token));
            }
            else
            {
                _playerStats.TakeHit(contact.normal.x / Mathf.Abs(contact.normal.x));
                Task.Run(async () => await HitCooldown());
            }

            _handlingCollision = false;
        }

        private void Update()
        {
            if (!_dead)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targets[_targetIndex].position, _speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _targets[_targetIndex].position) < .1f)
                {
                    _targetIndex++;
                    if (_targetIndex >= _targets.Count)
                    {
                        _targetIndex = 0;
                    }
                }

                return;
            }

            transform.localScale = _tagetScale;
            if (_tagetScale == Vector3.zero)
                Destroy(this.gameObject);
        }

        private async Task DeathAnim(CancellationToken token)
        {
            while (_tagetScale.magnitude > .2f)
            {
                await Task.Delay(TimeSpan.FromSeconds(.1));

                if (token.IsCancellationRequested)
                    return;

                _tagetScale = Vector3.Lerp(_tagetScale, Vector3.zero, .5f);
            }

            _tagetScale = Vector3.zero;
        }

        private async Task HitCooldown()
        {
            await Task.Delay(TimeSpan.FromSeconds(.1));
            _hitCooldown = false;
        }
    }
}
