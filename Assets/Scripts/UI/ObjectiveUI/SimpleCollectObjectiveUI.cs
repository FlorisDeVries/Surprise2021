using System;
using Objectives;
using TMPro;
using UnityEngine;

namespace UI.ObjectiveUI
{
    public class SimpleCollectObjectiveUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _collectedText;
        [SerializeField] private TMP_Text _targetText;
        [SerializeField] private SimpleCollectObjective _objective;

        private void OnEnable()
        {
            _objective.ObjectiveProgressed += UpdateUI;
            _targetText.text = _objective.ToCollect.ToString();

            _collectedText.text = _objective.Collected.ToString();
        }
        
        private void OnDisable()
        {
            _objective.ObjectiveProgressed -= UpdateUI;
        }

        private void UpdateUI()
        {
            _collectedText.text = _objective.Collected.ToString();
        }
    }
}
