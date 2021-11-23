using ScriptableObjects.Stats;
using UnityEngine;

namespace UI
{
    public class GameOverCanvas : MonoBehaviour
    {
        [Header("Player Properties")]
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private CanvasGroup _canvasGroup;

        // TODO: Let GameManager manage these canvases, instead of the player
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
}
