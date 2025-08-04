using ATrainGamePrototype.Input;
using TMPro;
using UnityEngine;

namespace ATrainGamePrototype.Scripts
{
    public class RegulatorHandler : MonoBehaviour, IInteractiveUIElement
    {
        [SerializeField] private TextMeshProUGUI _accelerationDisplay;
        
        [Header("Parameters")]
        [SerializeField] private float _sensitivity = 0.2f;
        [SerializeField] private float _minAngle = 0f;   
        [SerializeField] private float _maxAngle = 110f;
        
        
        public bool IsSelected { get; set; }
        
        private RectTransform rectTransform;
        private float _currentAngle = 0f;
        
        public void OnMousePress() {
        }

        public void OnMouseRelease() {
        }

        public void OnMouseMoveDelta(Vector2 delta) {
            if(!IsSelected) return;
            HandleRotation(delta);
        }

        private void HandleRotation(Vector2 delta) {
            float deltaAngle = delta.y * _sensitivity;

            _currentAngle = Mathf.Clamp(_currentAngle + deltaAngle, _minAngle, _maxAngle);
            rectTransform.localRotation = Quaternion.Euler(0, 0, _currentAngle);
            TrainController.I.SetAcceleration(RemapAngle(_currentAngle));
        }

        private float RemapAngle(float currentAngle) {
            float normalisedAcceleration = Mathf.InverseLerp(_minAngle, _maxAngle, currentAngle);
            _accelerationDisplay.text = normalisedAcceleration.ToString("F1");
            return normalisedAcceleration;
        }

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}
