using System;
using System.Collections.Generic;
using ScriptableObjects.Stats;
using UnityEngine;
using UnityEngine.Events;
using Util;

namespace ScriptableObjects
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
        
        public GameState State { get; private set; } = GameState.Playing;

        [SerializeField] private ObjectiveManagerSO _objectiveManager;
        public UnityAction<GameState> GameStateChangedEvent = delegate { };

        public void ChangeState(GameState targetState)
        {
            State = targetState;
            GameStateChangedEvent.Invoke(State);
        }

        private void OnEnable()
        {
            Debug.Log("Starting Level");
            _objectiveManager.StartLevel();
            _player.StartLevel();
        }
    }
}