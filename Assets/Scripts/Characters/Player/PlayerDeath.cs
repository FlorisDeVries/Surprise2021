using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Stats;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private SkeletonAnimation _characterCell = null;

    private void OnEnable()
    {
        _stats.PlayerDeathEvent += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _stats.PlayerDeathEvent -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        _characterCell.AnimationState.SetAnimation(0, "death", false);
    }
}
