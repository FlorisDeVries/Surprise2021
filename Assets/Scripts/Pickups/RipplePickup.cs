using System;
using ScriptableObjects.Stats;
using UnityEngine;

namespace Pickups
{
    public class RipplePickup : MonoBehaviour
    {
        [Header("Player Properties")] [SerializeField]
        private PlayerStats _stats;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }
            _stats.CollectRipple();
            Destroy(this.gameObject);
        }
    }
}
