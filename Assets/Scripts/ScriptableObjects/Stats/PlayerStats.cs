using Game;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Stats
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Characters/Player/Player Stats")]
    public class PlayerStats : BaseStats
    {
        [Header("Player shared components")]
        [SerializeField] private InputHandler _inputHandler = default;
        public InputHandler InputHandler => _inputHandler;

        // In Game Stats
        public float CurrentHealth { get; private set; } = 0;
        public bool IsAlive => CurrentHealth > 0;
        
        private int _rippleScore = 0;

        public event UnityAction<float> PlayerHitEvent = delegate { };
        public event UnityAction KilledEnemyEvent = delegate { };
        public event UnityAction PlayerDeathEvent = delegate { };
        
        public event UnityAction<int> RippleCollectedEvent = delegate { };

        public void StartLevel()
        {
            Reset();
        }
        
        private void Reset()
        {
            CurrentHealth = maxHealth;
            _rippleScore = 0;
        }

        public void TakeHit(float direction)
        {
            if (CurrentHealth <= 0)
                return;

            CurrentHealth--;
            if (CurrentHealth <= 0)
            {
                PlayerDeathEvent.Invoke();
                GameManager.Instance.SetGameState(GameState.GameOver);
                PlayerHitEvent.Invoke(direction);
            }
            else
            {
                PlayerHitEvent.Invoke(direction);
            }
        }

        public void InstaKill()
        {
            CurrentHealth = 0;
            PlayerDeathEvent.Invoke();
            GameManager.Instance.SetGameState(GameState.GameOver);
        }

        public void KilledEnemy()
        {
            KilledEnemyEvent.Invoke();
        }

        public void CollectRipple()
        {
            _rippleScore++;
            RippleCollectedEvent.Invoke(_rippleScore);
        }
    }
}
