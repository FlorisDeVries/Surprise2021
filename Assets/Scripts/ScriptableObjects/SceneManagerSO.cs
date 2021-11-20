using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects
{
    public enum Scene
    {
        MainMenu,
        Tutorial,
        Level1
    }

    [CreateAssetMenu(fileName = "SceneManager", menuName = "Managers/SceneManager")]
    public class SceneManagerSO : ScriptableObject
    {
        public void ActivateScene(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }

        public void ActivateScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        internal void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}