using Pickups;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Objectives
{
    public interface IObjective
    {
        public ObjectiveType GetObjectiveType();
        public void Collect(ICollectable collectable);
        public bool IsComplete();
        public void Reset();
    }
    
    public abstract class AObjective : ScriptableObject, IObjective
    {
        [SerializeField] protected ObjectiveType type = ObjectiveType.None;
        public UnityAction ObjectiveProgressed = delegate { };
        
        public ObjectiveType GetObjectiveType()
        {
            return type;
        }

        public abstract void Collect(ICollectable collectable);
        public abstract bool IsComplete();
        public abstract void Reset();
    }
}