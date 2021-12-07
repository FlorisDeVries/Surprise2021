using Game;
using ScriptableObjects;
using ScriptableObjects.Stats;
using UnityEngine;

namespace World.Pickups
{
    public class RipplePickup : ACollectable
    {        
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private GameObject _particles;

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
            Instantiate(_particles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
