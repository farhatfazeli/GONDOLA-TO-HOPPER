using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace TrainGame.View.Resource
{
    public class PassengerResourceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _passengerResource;

        private readonly float _time = SO_GameParameters.I.resourceAnimationTick;

        private int _value;

        private int Value
        {
            get => _value;
            set
            {
                RefreshView(value);
                _value = value;
            }
        }
        
        private Coroutine _animationCoroutine;
        
        private void RefreshView(int newValue)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }
            
            _animationCoroutine = StartCoroutine(animateResource(newValue));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Value += 50;
            }
        }

        private IEnumerator animateResource(int newValue)
        {
            WaitForSeconds wait = new WaitForSeconds(_time);
            
            int previousValue = _value;
            int stepAmount;

            if (newValue - previousValue < 0)
            {
                stepAmount = Mathf.FloorToInt((newValue - previousValue) * _time);
            }
            else
            {
                stepAmount = Mathf.CeilToInt((newValue - previousValue) * _time);
            }

            if (previousValue < newValue)
            {
                while (previousValue < newValue)
                {
                    previousValue += stepAmount;
                    if (previousValue > newValue)
                    {
                        previousValue = newValue;
                    }

                    _passengerResource.SetText(previousValue.ToString());

                    yield return wait;
                }
            }
            else if (previousValue > newValue)
            {
                while (previousValue < newValue)
                {
                    previousValue += stepAmount;
                    if (previousValue < newValue)
                    {
                        previousValue = newValue;
                    }
                    
                    _passengerResource.SetText(previousValue.ToString());

                    yield return wait;
                }
            }
        }
    }
}