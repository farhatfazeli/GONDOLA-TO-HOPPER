using System.Collections.Generic;
using ATrainGamePrototype.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ATrainGamePrototype.Input
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private RegulatorHandler _regulatorHandler;
        [SerializeField] private WhistleHandler _whistleHandler;
        [SerializeField] private FireboxDoorHandler _fireboxDoorHandler;
    
        private bool _stateActive;
        private TrainUIInputMap _map;
    
        private GraphicRaycaster _graphicRaycaster;
        private List<IInteractiveUIElement> _interactiveUIElements = new List<IInteractiveUIElement>();


        private void Awake() {
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
        }
    
        public void EnableControllerInput(TrainUIInputMap map) {
            if(_stateActive) return;
            //Debug.Log("Train UI Mode Active");
            map.MouseHold += OnMouseHold;
            map.MouseMoveDelta += _regulatorHandler.OnMouseMoveDelta;
            map.MouseMoveDelta += _whistleHandler.OnMouseMoveDelta;
            map.MouseMoveDelta += _fireboxDoorHandler.OnMouseMoveDelta;
            map.EnablePlayerActions();
            _map = map;
            _stateActive = true;
        }

        private void OnMouseHold(bool isPressed) {
            switch (isPressed) {
                case true:
                    PointerEventData pointerData = new PointerEventData(_eventSystem) {
                        position = _map.MousePosition
                    };

                    List<RaycastResult> raycastResults = new List<RaycastResult>();
                    _graphicRaycaster.Raycast(pointerData, raycastResults);
                    foreach (var result in raycastResults)
                    {
                        Debug.Log(result.gameObject.name);
                        IInteractiveUIElement _IUIElement = result.gameObject.GetComponent<IInteractiveUIElement>();
                        if(_IUIElement == null) continue;
                        _interactiveUIElements.Add(_IUIElement);
                        _IUIElement.IsSelected = true;
                        _IUIElement.OnMousePress();
                        break;
                    }
                    break;
                case false:
                    foreach (var interactiveUIElement in _interactiveUIElements)
                    {
                        interactiveUIElement.IsSelected = false;
                        interactiveUIElement.OnMouseRelease();
                    }
                    _interactiveUIElements.Clear();
                    break;
                    
                //     RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_map.MousePosition), Vector2.zero);
                //     if (hit.collider == null || hit.rigidbody == null)
                //         return;
                //     if (hit.transform.TryGetComponent(out PickupHandler pickupHandler)) {
                //         _pickupHandler = pickupHandler;
                //         _pickupHandler.GetPickedUp();
                //     }
                //     break;
                // case false:
                //     if (_pickupHandler == null)
                //         return;
                //     _pickupHandler.GetReleased();
                //     _pickupHandler = null;
                //     break;
            
            }
        }

        public void DisableControllerInput() {
            if(!_stateActive) return;
            _map.MouseHold -= OnMouseHold;
            _map.MouseMoveDelta -= _regulatorHandler.OnMouseMoveDelta;
            _map.MouseMoveDelta -= _whistleHandler.OnMouseMoveDelta;
            _map.MouseMoveDelta -= _fireboxDoorHandler.OnMouseMoveDelta;
            _map.DisablePlayerActions();
            _stateActive = false;
        }
    }

    public interface IInteractiveUIElement
    {
        public bool IsSelected { get; set; }
        
        public void OnMousePress();
        public void OnMouseRelease();
        public void OnMouseMoveDelta(Vector2 delta);
    }
}