using System;

namespace Core.Utility
{
    public class BaseResource
    {
        private float _value;
        
        public float value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged.Invoke(_value);
            }
        }

        public event Action<float> OnValueChanged =  delegate { };
    }
}