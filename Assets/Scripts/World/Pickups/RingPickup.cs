using ScriptableObjects;
using UnityEngine;

namespace World.Pickups
{
    public class RingPickup : ACollectable
    {
        private void OnEnable()
        {
            type = ObjectiveType.Ring;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }

            RegisterCollect();
            Destroy(this.gameObject);
        }
    }
}
