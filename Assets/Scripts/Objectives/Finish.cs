using System;
using Game;
using ScriptableObjects;
using UnityEngine;

namespace Objectives
{
    public class Finish : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                return;

            if (!GameManager.Instance.ObjectiveManager.IsComplete())
                return;

            GameManager.Instance.SetGameState(GameState.Victory);
        }
    }
}
