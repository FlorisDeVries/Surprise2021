using System;
using Game;
using ScriptableObjects;
using UnityEngine;
using World;

namespace Characters.Player
{
    public class PlayerWorldInteractor : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private BoxCollider2D _collisionRange;
        [SerializeField] private LayerMask _interactableLayers;
        
        private void OnEnable()
        {
            _collisionRange.enabled = false;
            _inputHandler.InteractEvent += CheckWorldInteractions;
        }

        private void OnDisable()
        {
            _collisionRange.enabled = true;
            _inputHandler.InteractEvent -= CheckWorldInteractions;
        }

        private void CheckWorldInteractions()
        {
            var pos = new Vector2(transform.position.x, transform.position.y) + _collisionRange.offset;
            var results = new Collider2D[2];
            var size = Physics2D.OverlapBoxNonAlloc(pos, _collisionRange.size, 0, results, _interactableLayers);

            for (var i = 0; i < size; i++)
            {
                var interactable = results[i].GetComponent<Interactable>();
                interactable.Interact();
            }
        }
    }
}
