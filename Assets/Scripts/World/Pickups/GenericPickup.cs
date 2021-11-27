using ScriptableObjects;
using UnityEngine;

namespace World.Pickups
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GenericPickup : ACollectable
    {
        [SerializeField] private ObjectiveType _type;
        
        private void OnEnable()
        {
            type = _type;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }

            RegisterCollect();
            Destroy(gameObject);
        }
    }
}
