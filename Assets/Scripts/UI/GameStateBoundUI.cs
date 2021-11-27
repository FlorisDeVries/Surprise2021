using Game;
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
            _canvasGroup.interactable = GameManager.Instance.State == _visibleGameState;
            _canvasGroup.blocksRaycasts = GameManager.Instance.State == _visibleGameState;
        }

        private void OnDisable()
        {
            GameManager.Instance.GameStateChangedEvent -= OnGameStateChanged;
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void OnGameStateChanged(GameState newState)
        {
            _canvasGroup.alpha = newState != _visibleGameState ? 0 : 1;
            _canvasGroup.interactable = newState == _visibleGameState;
            _canvasGroup.blocksRaycasts = newState == _visibleGameState;
        }
    }
}