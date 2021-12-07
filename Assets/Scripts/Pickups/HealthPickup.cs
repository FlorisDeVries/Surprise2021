using System;
using Game;
using UnityEngine;

namespace Pickups
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] private GameObject _particles;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }
            
            GameManager.Instance.Player.Heal();
            if (_particles != null)
                Instantiate(_particles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
