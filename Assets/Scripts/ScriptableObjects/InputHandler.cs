using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "InputHandler", menuName = "Gameplay/Input Handler")]
    public class InputHandler : ScriptableObject, GameplayActions.IInGameActions
    {
        public event UnityAction<float> LeftRightEvent = delegate { };
        public event UnityAction<bool> JumpEvent = delegate { };
        public event UnityAction PauseEvent = delegate { };
        public event UnityAction InteractEvent = delegate { };

        private GameplayActions _gameInput;

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameplayActions();
                _gameInput.InGame.SetCallbacks(this);
            }

            // Enable desire input scheme
            _gameInput.InGame.Enable();
        }

        private void OnDisable()
        {
            // Disable all input schemes
            _gameInput.InGame.Disable();
        }

        public void OnLeftRight(InputAction.CallbackContext context)
        {
            LeftRightEvent.Invoke(context.ReadValue<float>());
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpEvent.Invoke(true);
            }
            else if (context.canceled)
            {
                JumpEvent.Invoke(false);
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                PauseEvent.Invoke();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                InteractEvent.Invoke();
            }
        }
    }
}
