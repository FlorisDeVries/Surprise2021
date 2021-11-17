using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GoombaEnemy : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private BoxCollider2D _collider;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite _deadSprite;

    public string isDyingStatus = "";
    private Vector3 _tagetScale = Vector3.one;
    private bool _handlingCollision = false;
    private bool _dead = false;

    private CancellationTokenSource deathAnimToken = new CancellationTokenSource();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" || _handlingCollision || _dead)
            return;

        _handlingCollision = true;

        var contact = collision.GetContact(0);
        if (Mathf.Abs(contact.normal.y) > .5f)
        {
            // Die
            _dead = true;
            _collider.enabled = false;
            _playerStats.KilledEnemy();
            _renderer.sprite = _deadSprite;

            // TODO: Play Death animation
            isDyingStatus = "Dying";
            deathAnimToken = new CancellationTokenSource();
            Task.Run(async () => await DeathAnim(deathAnimToken.Token));
        }
        else
        {
            _playerStats.TakeHit(contact.normal.x / Mathf.Abs(contact.normal.x));
        }

        _handlingCollision = false;
    }

    private void Update()
    {
        if (!_dead)
            return;

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
}
