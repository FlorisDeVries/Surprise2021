using System;
using ScriptableObjects.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class RippleScoreUI : MonoBehaviour
    {
        [Header("Player Properties")] [SerializeField]
        private PlayerStats _stats;

        [SerializeField] private TMP_Text _counter;

        private void OnEnable()
        {
            _stats.RippleCollectedEvent += SyncScore;
        }

        private void OnDisable()
        {
            _stats.RippleCollectedEvent -= SyncScore;
        }

        private void SyncScore(int score)
        {
            _counter.text = score.ToString();
        }
    }
}