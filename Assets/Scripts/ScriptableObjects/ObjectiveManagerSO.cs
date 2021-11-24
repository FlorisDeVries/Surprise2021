using System;
using System.Collections.Generic;
using System.Linq;
using Objectives;
using Pickups;
using UnityEngine;

namespace ScriptableObjects
{
    public enum ObjectiveType
    {
        None,
        Ripple
    }

    [CreateAssetMenu(fileName = "ObjectiveManager", menuName = "Managers/ObjectiveManager")]
    public class ObjectiveManagerSO : ScriptableObject
    {
        [SerializeField] private List<AObjective> _objectives;

        private Dictionary<ObjectiveType, AObjective> _objectivesDict;

        public void StartLevel()
        {
            _objectivesDict = new Dictionary<ObjectiveType, AObjective>();
            foreach (var aObjective in _objectives)
            {
                aObjective.Reset();
                _objectivesDict.Add(aObjective.GetObjectiveType(), aObjective);
            }
        }

        public void Collect(ICollectable collectable)
        {
            var type = collectable.GetObjectiveType();
            
            if (_objectivesDict.ContainsKey(type))
            {
                _objectivesDict[type].Collect(collectable);
            }
        }

        public bool IsComplete()
        {
            return _objectives.All(x => x.IsComplete());
        }
    }
}