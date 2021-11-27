using System;
using UnityEngine;
using World.Pickups;

namespace Objectives
{
    [CreateAssetMenu(fileName = "BlockedCollectObjective", menuName = "Objectives/Blocked Collect")]
    public class BlockedCollectObjective : AObjective
    {
        [SerializeField] private AObjective _condition;

        private bool _complete = false;

        private void OnEnable()
        {
            _condition.ObjectiveProgressed += () => ObjectiveProgressed.Invoke();
        }

        private void OnDisable()
        {
            _condition.ObjectiveProgressed -= () => ObjectiveProgressed.Invoke();
        }

        public bool IsUnlocked()
        {
            return _condition.IsComplete();
        }

        public override void Collect(ICollectable collectable)
        {
            if (!IsUnlocked())
                return;

            _complete = true;
            ObjectiveProgressed.Invoke();
        }

        public override bool IsComplete()
        {
            return _complete;
        }

        public override void Reset()
        {
            _complete = false;
            ObjectiveProgressed.Invoke();
        }
    }
}