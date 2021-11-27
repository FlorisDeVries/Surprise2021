using Game;
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
        protected ObjectiveType type = ObjectiveType.None;

        protected void RegisterCollect()
        {
            GameManager.Instance.ObjectiveManager.Collect(this);
        }

        public ObjectiveType GetObjectiveType()
        {
            return type;
        }
    }
    
}