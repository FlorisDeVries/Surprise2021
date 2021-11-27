using Objectives;
using UnityEngine;

namespace World.Pickups
{
    public class BlockedCollectable : GenericCollectable
    {
        [SerializeField] private AObjective _blockedBy;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!_blockedBy.IsComplete())
                return;

            base.OnTriggerEnter2D(other);
        }
    
    }
}
