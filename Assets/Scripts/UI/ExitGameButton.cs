using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ExitGameButton : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Application.Quit();
        }
    }
}
