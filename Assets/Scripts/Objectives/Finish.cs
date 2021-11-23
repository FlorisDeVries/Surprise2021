using System;
using ScriptableObjects;
using UnityEngine;

namespace Objectives
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private ObjectiveManagerSO _objectiveManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                return;

            if (!_objectiveManager.IsComplete())
                return;
            
            // TODO: Notify GameManager that the level is complete
        }
    }
}
