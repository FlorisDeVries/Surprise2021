using UnityEngine;

namespace Util
{
    [ExecuteInEditMode]
    public class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;
        
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;
                
                var singletonObject = new GameObject(typeof(T).ToString());
                _instance = singletonObject.AddComponent<T>();
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = GetComponent<T>();
        }
    }
}