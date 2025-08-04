using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ATrainGamePrototype.Input
{
    [CreateAssetMenu(fileName = "TrainUIInputMap", menuName = "Scriptable Objects/UI/TrainUIInputMap")]
    public class TrainUIInputMap : ScriptableObject, TrainUIInput.ITrainUIMapActions
    {
        public event Action MousePress = delegate { };
        public event Action<bool> MouseHold = delegate { };
        public event Action<Vector2> MouseMoveDelta = delegate { };

        public bool MouseIsPressed => trainUIInput.TrainUIMap.MousePress.IsPressed();
        public Vector2 MousePosition => trainUIInput.TrainUIMap.MousePosition.ReadValue<Vector2>();
        public Vector2 MouseDelta => trainUIInput.TrainUIMap.MouseDelta.ReadValue<Vector2>();

        public void OnMousePosition(InputAction.CallbackContext context) {
        }

        public void OnMouseDelta(InputAction.CallbackContext context) {
            MouseMoveDelta.Invoke(context.ReadValue<Vector2>());
        }

        public void OnMousePress(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    MousePress.Invoke();
                    MouseHold.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    MouseHold.Invoke(false);
                    break;
            }
        }
        
        private TrainUIInput trainUIInput;

        public void EnablePlayerActions() {
            if (trainUIInput == null) {
                trainUIInput = new TrainUIInput();
                trainUIInput.TrainUIMap.SetCallbacks(this);
            }
            trainUIInput.TrainUIMap.Enable();
        }

        public void DisablePlayerActions() {
            if (trainUIInput == null) return;
            trainUIInput.TrainUIMap.RemoveCallbacks(this);
                
            trainUIInput.TrainUIMap.Disable();
            trainUIInput = null;
        }
    }
}