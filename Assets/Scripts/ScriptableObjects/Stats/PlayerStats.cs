using Assets.Scripts.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Characters/Player/Player Stats")]
public class PlayerStats : BaseStats
{
    [Header("Player shared components")]
    [SerializeField] private InputHandler _inputHandler = default;
    public InputHandler InputHandler => _inputHandler;

    // In Game Stats
    public float CurrentHealth { get; private set; } = 0;
    public bool IsAlive => CurrentHealth > 0;

    public event UnityAction<float> PlayerHitEvent = delegate { };
    public event UnityAction KilledEnemyEvent = delegate { };
    public event UnityAction PlayerDeathEvent = delegate { };

    private void OnEnable()
    {
        Reset();
    }

    private void Reset()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeHit(float direction)
    {
        if (CurrentHealth <= 0)
            return;

        CurrentHealth--;
        if (CurrentHealth <= 0)
        {
            PlayerDeathEvent.Invoke();
            PlayerHitEvent.Invoke(direction);
        }
        else
        {
            PlayerHitEvent.Invoke(direction);
        }
    }

    public void KilledEnemy()
    {
        KilledEnemyEvent.Invoke();
    }
}
