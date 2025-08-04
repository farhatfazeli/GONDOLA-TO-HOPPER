using System.Collections;
using ScriptableObjects;
using TMPro;
using TrainGame.Model.Resource;
using UnityEngine;

namespace TrainGame.View.Resource
{
    public class PassengerResourceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _passengerResource;

        private float _time;

        private int _previousValue;
        
        private Coroutine _animationCoroutine;

        private void Start()
        {
            _time = SO_GameParameters.I.resourceAnimationTick;
            
            _previousValue = ResourceManager.I.passengerResource;
            ResourceManager.I.OnPassengerResourceUpdated += RefreshView;

            SetResourceText();
        }

        private void RefreshView(int newValue)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }
            
            _animationCoroutine = StartCoroutine(animateResource(newValue));
        }

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.K))
        //     {
        //         ResourceManager.I.GainResource(50, ResourceType.Passengers);
        //     }
        //     else if (Input.GetKeyDown(KeyCode.L))
        //     {
        //         ResourceManager.I.SpendResource(30,  ResourceType.Passengers);
        //     }
        // }

        private IEnumerator animateResource(int newValue)
        {
            WaitForSeconds wait = new WaitForSeconds(_time);
            
            int stepAmount;

            if (newValue < _previousValue)
            {
                stepAmount = Mathf.FloorToInt((newValue - _previousValue) * _time);
            }
            else
            {
                stepAmount = Mathf.CeilToInt((newValue - _previousValue) * _time);
            }

            if (_previousValue < newValue)
            {
                while (_previousValue < newValue)
                {
                    _previousValue += stepAmount;
                    if (_previousValue > newValue)
                    {
                        _previousValue = newValue;
                    }

                    SetResourceText();

                    yield return wait;
                }
            }
            else if (_previousValue > newValue)
            {
                while (_previousValue > newValue)
                {
                    _previousValue += stepAmount;
                    if (_previousValue < newValue)
                    {
                        _previousValue = newValue;
                    }
                    
                    SetResourceText();

                    yield return wait;
                }
            }
        }

        private void SetResourceText()
        {
            _passengerResource.SetText(_previousValue.ToString());
        }
    }
}