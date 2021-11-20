using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartSceneButton : MonoBehaviour
    {
        [SerializeField] private SceneManagerSO sceneManager;
        [SerializeField] private bool _currentScene = false;
        [SerializeField] private Scene _sceneToLoad = Scene.MainMenu;

        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (_currentScene)
                sceneManager.ReloadScene();
            else
                sceneManager.ActivateScene(_sceneToLoad);
        }
    }
}
