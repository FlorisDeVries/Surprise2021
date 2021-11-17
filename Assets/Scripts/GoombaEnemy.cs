using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaEnemy : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private BoxCollider2D _collider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        var contact = collision.GetContact(0);
        if (Mathf.Abs(contact.normal.y) > .5f)
        {
            // Die
            _playerStats.KilledEnemy();
            //_collider.enabled = false;
        }
        else
        {
            _playerStats.TakeHit(contact.normal.x / Mathf.Abs(contact.normal.x));
        }
    }
}
