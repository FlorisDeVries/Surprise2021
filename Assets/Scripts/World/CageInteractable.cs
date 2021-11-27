using UnityEngine;

namespace World
{
    public class CageInteractable : Interactable
    {
        public override void Interact()
        {
            base.Interact();
            
            Destroy(this.gameObject);
        }
    }
}
