using System;
using ScriptableObjects.Stats;
using UnityEngine;

namespace World
{
    public class DeathZone : MonoBehaviour
    {
        [Header("Player Properties")]
        [SerializeField] private PlayerStats _playerStats;
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }
            
            _playerStats.InstaKill();
        }
    }
}
