using Pickups;
using UnityEngine;

namespace Objectives
{
    [CreateAssetMenu(fileName = "SimpleCollectObjective", menuName = "Objectives/Simple Collect")]
    public class SimpleCollectObjective : AObjective
    {
        [SerializeField] private int _amountToCollect = 5;
        public int ToCollect => _amountToCollect;
        public int Collected { get; private set; } = 0;


        public override void Collect(ICollectable collectable)
        {
            Collected++;
            ObjectiveProgressed.Invoke();
        }

        public override bool IsComplete()
        {
            return Collected >= _amountToCollect;
        }

        public override void Reset()
        {
            Collected = 0;
            ObjectiveProgressed.Invoke();
        }
    }
}