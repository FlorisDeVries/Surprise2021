using ScriptableObjects;
using UnityEngine;

namespace World.Pickups
{
    public interface ICollectable
    {
        public ObjectiveType GetObjectiveType();
    }

    public abstract class ACollectable: MonoBehaviour, ICollectable
    {
        [SerializeField] private ObjectiveManagerSO _objectiveManager;
        
        protected ObjectiveType type = ObjectiveType.None;

        protected void RegisterCollect()
        {
            _objectiveManager.Collect(this);
        }

        public ObjectiveType GetObjectiveType()
        {
            return type;
        }
    }
    
}