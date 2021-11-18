using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _canvasGroup.alpha = 0;
        _stats.PlayerDeathEvent += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _canvasGroup.alpha = 1;
        _stats.PlayerDeathEvent -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        _canvasGroup.alpha = 1;
    }
}
