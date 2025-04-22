using System;

namespace Core.Utility
{
    public abstract class BaseResource
    {
        protected int _value;
        
        public int value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged.Invoke(_value);
            }
        }

        public event Action<int> OnValueChanged =  delegate { };

        public BaseResource(int startValue)
        {
            _value = startValue;
        }
    }
}