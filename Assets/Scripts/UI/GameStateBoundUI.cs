using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class GameStateBoundUI : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        [SerializeField] private GameState _visibleGameState;

        private void OnEnable()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            GameManager.Instance.GameStateChangedEvent += OnGameStateChanged;
            _canvasGroup.alpha = GameManager.Instance.State != _visibleGameState ? 0 : 1;
        }

        private void OnDisable()
        {
            GameManager.Instance.GameStateChangedEvent -= OnGameStateChanged;
            _canvasGroup.alpha = 1;
        }

        private void OnGameStateChanged(GameState newState)
        {
            _canvasGroup.alpha = newState != _visibleGameState ? 0 : 1;
        }
    }
}