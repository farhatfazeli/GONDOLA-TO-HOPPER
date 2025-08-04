using System.Collections;
using ATrainGamePrototype.Input;
using TMPro;
using UnityEngine;

namespace ATrainGamePrototype.Scripts
{
    public class FireboxDoorHandler : MonoBehaviour, IInteractiveUIElement
    {
        [Header("Parameters")]
        [SerializeField] private float _sensitivity = 0.2f;
        [SerializeField] private float _minAngle = 0f;   
        [SerializeField] private float _maxAngle = 110f;
        
        [Header("Doors")]
        [SerializeField] private RectTransform _leftDoor;
        [SerializeField] private RectTransform _rightDoor;
        
        public bool IsSelected { get; set; }
        
        private RectTransform rectTransform;
        private float _startAngle;
        private float _currentAngle = 0;
        private float _leftDoorStartAngle = 0;
        private float _rightDoorStartAngle = 0;
        
        public void OnMousePress() {
        }

        public void OnMouseRelease() {
        }

        public void OnMouseMoveDelta(Vector2 delta) {
            if(!IsSelected) return;
            HandleRotation(delta);
        }

        private void HandleRotation(Vector2 delta) {
            float deltaAngle = -delta.y * _sensitivity;

            _currentAngle = Mathf.Clamp(_currentAngle + deltaAngle, _minAngle, _maxAngle);
            rectTransform.localRotation = Quaternion.Euler(0, 0, _startAngle + _currentAngle);
            _leftDoor.localRotation = Quaternion.Euler(0, 0, _leftDoorStartAngle - _currentAngle);
            _rightDoor.localRotation = Quaternion.Euler(0, 0, _rightDoorStartAngle + _currentAngle);
        }

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
            _startAngle = rectTransform.localEulerAngles.z;
            _leftDoorStartAngle = _leftDoor.localRotation.eulerAngles.z;
            _rightDoorStartAngle = _rightDoor.localRotation.eulerAngles.z;
        }
    }
}
