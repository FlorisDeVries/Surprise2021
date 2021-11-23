using System;
using Cinemachine;
using ScriptableObjects;
using ScriptableObjects.Stats;
using UnityEngine;

namespace Pickups
{
    public class RipplePickup : ACollectable
    {
        [Header("Player Properties")] [SerializeField]
        private PlayerStats _stats;

        private void OnEnable()
        {
            type = ObjectiveType.Ripple;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }

            RegisterCollect();
            _stats.CollectRipple();
            Destroy(this.gameObject);
        }
    }
}
