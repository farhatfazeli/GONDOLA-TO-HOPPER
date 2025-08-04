using System;
using ATrainGamePrototype.Input;
using UnityEngine;
using UnityEngine.UI;

namespace ATrainGamePrototype.Scripts
{
    public class FireboxFireHandler : MonoBehaviour, IInteractiveUIElement
    {
        public bool IsSelected { get; set; }

        private float _coalLevel;
        private Image _fireImage;
        
        public void OnMousePress() {
            Debug.Log("OnMousePressFIREFIRE");
            AddCoal(0.2f);
        }

        private void AddCoal(float amount) {
            _coalLevel = Mathf.Clamp(_coalLevel + amount, 0f, 1f);
            UpdateCoalDisplay();
        }

        private void BurnCoal(float amount) {
            AddCoal(-amount);
        }
        
        private void UpdateCoalDisplay() {
            _fireImage.color = new Color(1f * _coalLevel, 1f * _coalLevel, 1f * _coalLevel);
        }
        
        private void Update() {
            BurnCoal(0.05f * Time.deltaTime);
        }

        private void Awake() {
            _fireImage = GetComponent<Image>();
            UpdateCoalDisplay();
        }

        public void OnMouseRelease() {
        }

        public void OnMouseMoveDelta(Vector2 delta) {
        }
    }
}
