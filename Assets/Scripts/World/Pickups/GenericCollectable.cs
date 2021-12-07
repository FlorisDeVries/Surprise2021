using ScriptableObjects;
using UnityEngine;

namespace World.Pickups
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GenericCollectable : ACollectable
    {
        [SerializeField] private ObjectiveType _type;
        [SerializeField] private GameObject _particles;
        [SerializeField] private Vector3 _particlesOffset;
        
        private void OnEnable()
        {
            type = _type;
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }

            RegisterCollect();
            if (_particles != null)
                Instantiate(_particles, transform.position + _particlesOffset, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
