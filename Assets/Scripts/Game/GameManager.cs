using System;
using ScriptableObjects;
using ScriptableObjects.Stats;
using UnityEngine;
using UnityEngine.Events;
using Util;

namespace Game
{
    public enum GameState
    {
        GameOver,
        Victory,
        Playing,
        Paused
    }

    public class GameManager : UnitySingleton<GameManager>
    {
        [SerializeField] private PlayerStats _player;
        [SerializeField] private InputHandler _input;

        public GameState State { get; private set; } = GameState.Playing;

        [SerializeField] private ObjectiveManagerSO _objectiveManager;
        public ObjectiveManagerSO ObjectiveManager => _objectiveManager;
        public UnityAction<GameState> GameStateChangedEvent = delegate { };

        public void SetGameState(GameState targetState)
        {
            State = targetState;
            GameStateChangedEvent.Invoke(State);
        }

        private void OnEnable()
        {
            Debug.Log("Starting Level");
            _objectiveManager.StartLevel();
            _player.StartLevel();

            _input.PauseEvent += TogglePause;
        }

        private void OnDisable()
        {
            Debug.Log("Stopped Level");
            
            _input.PauseEvent -= TogglePause;
        }

        public void TogglePause()
        {
            SetGameState(State == GameState.Paused ? GameState.Playing : GameState.Paused);
        }
    }
}