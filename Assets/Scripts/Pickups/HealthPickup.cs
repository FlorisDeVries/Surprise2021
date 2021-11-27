using System;
using Game;
using UnityEngine;

namespace Pickups
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class HealthPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }
            
            GameManager.Instance.Player.Heal();
            Destroy(this.gameObject);
        }
    }
}
