using System.Collections.Generic;
using ScriptableObjects.Stats;
using UnityEngine;

namespace UI
{
    public class PlayerHpUI : MonoBehaviour
    {
        [Header("Player Properties")]
        [SerializeField] private PlayerStats _stats;

        [SerializeField] private List<GameObject> _heartSprites;

        private void OnEnable()
        {
            _stats.PlayerHitEvent += SyncUI;
            _stats.PlayerHealedEvent += SyncUI;

            _heartSprites = new List<GameObject>();
            for (var i = 0; i < transform.childCount; i++)
            {
                _heartSprites.Add(transform.GetChild(i).gameObject);
            }
            SyncUI();
        }

        private void OnDisable()
        {
            _stats.PlayerHitEvent -= SyncUI;
            _stats.PlayerHealedEvent -= SyncUI;
        }

        private void SyncUI()
        {
            for (var i = 0; i < _heartSprites.Count; i++)
            {
                _heartSprites[i].SetActive(i < _stats.CurrentHealth);
            }
        }

        private void SyncUI(float _)
        {
            SyncUI();
        }
        
    }
}