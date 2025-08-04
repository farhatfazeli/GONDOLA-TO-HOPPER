using System.Collections;
using ATrainGamePrototype.Input;
using TMPro;
using UnityEngine;

namespace ATrainGamePrototype.Scripts
{
    public class WhistleHandler : MonoBehaviour, IInteractiveUIElement
    {
        [SerializeField] private TextMeshProUGUI _displacementDisplay;
        
        [Header("Parameters")]
        [SerializeField] private float _sensitivity = 0.2f;
        [SerializeField] private float _minDisplacement;   
        [SerializeField] private float _maxDisplacement = 30f;
        
        public bool IsSelected { get; set; }
        
        private RectTransform rectTransform;
        private float _currentDisplacement = 0f;
        private bool _activated;

        public void OnMousePress() {
            StopAllCoroutines();
        }

        public void OnMouseRelease() {
            StartCoroutine(MoveToOriginalPosition());
        }

        private IEnumerator MoveToOriginalPosition() {
            float start = _currentDisplacement;
            float end = _minDisplacement;
            float t = 0f;

            while (t < 1f) {
                t += Time.deltaTime;
                _currentDisplacement = Mathf.Lerp(start, end, t);
                UpdatePosition();
                yield return null;
            }
            
            _currentDisplacement = end;
        }

        public void OnMouseMoveDelta(Vector2 delta) {
            if(!IsSelected) return;
            HandleDisplacement(delta);
        }

        private void HandleDisplacement(Vector2 delta) {
            float deltaY = delta.y * _sensitivity;
            _currentDisplacement = Mathf.Clamp(_currentDisplacement + deltaY, _maxDisplacement, _minDisplacement);
            UpdatePosition();
        }
        
        private float RemapDisplacement(float currentDisplacement) {
            float normalisedDisplacement = Mathf.InverseLerp(_minDisplacement, _maxDisplacement, currentDisplacement);
            _displacementDisplay.text = normalisedDisplacement.ToString("F1");
            return normalisedDisplacement;
        }
        
        private void UpdatePosition() {
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, _currentDisplacement, 0);
            TrainController.I.BlowWhistle(RemapDisplacement(_currentDisplacement));
        }
        
        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
            _minDisplacement = rectTransform.position.y;
            _maxDisplacement = _minDisplacement - _maxDisplacement;
        }
    }
}
