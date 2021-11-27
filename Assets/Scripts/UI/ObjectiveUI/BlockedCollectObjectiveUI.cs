using Objectives;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ObjectiveUI
{
    public class BlockedCollectObjectiveUI : MonoBehaviour
    {
        [SerializeField] private BlockedCollectObjective _objective;

        [Header("UI elements")] 
        [SerializeField] private Image _blockedByImage;
        [SerializeField] private Image _completedImage;
        
        private void OnEnable()
        {
            _objective.ObjectiveProgressed += UpdateUI;

            _blockedByImage.enabled = true;
            _completedImage.enabled = false;
        }
        
        private void OnDisable()
        {
            _objective.ObjectiveProgressed -= UpdateUI;
        }

        private void UpdateUI()
        {
            Debug.Log("Progressed");
            _blockedByImage.enabled = !_objective.IsUnlocked();
            _completedImage.enabled = _objective.IsComplete();
        }
    }
}
