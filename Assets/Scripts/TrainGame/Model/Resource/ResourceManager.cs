using System;

namespace TrainGame.Model.Resource
{
    public class ResourceManager
    {
        public int passengerResource => _passengerResource.value;
        public int freightResource => _freightResource.value;
        
        public event Action<int> OnPassengerResourceUpdated
        {
            add => _passengerResource.OnValueChanged += value;
            remove => _passengerResource.OnValueChanged -= value;
        }
        
        public event Action<int> OnFreightResourceUpdated
        {
            add => _freightResource.OnValueChanged += value;
            remove => _passengerResource.OnValueChanged -= value;
        }

        private readonly PassengerResource _passengerResource;
        private readonly FreightResource _freightResource;

        public void GainPassengerResource(int value)
        {
            switch (value)
            {
                case < 0:
                    throw new ArithmeticException();
                case 0:
                    return;
                default:
                    _passengerResource.value += value;
                    break;
            }
        }

        public void SpendPassengerResource(int value)
        {
            switch (value)
            {
                case < 0:
                    throw new ArithmeticException();
                case 0:
                    return;
                default:
                    _passengerResource.value -= value;
                    break;
            }
        }

        public void GainFreightResource(int value)
        {
            switch (value)
            {
                case < 0:
                    throw new ArithmeticException();
                case 0:
                    return;
                default:
                    _freightResource.value += value;
                    break;
            }
        }

        public void SpendFreightResource(int value)
        {
            switch (value)
            {
                case < 0:
                    throw new ArithmeticException();
                case 0:
                    return;
                default:
                    _freightResource.value -= value;
                    break;
            }
        }
        
        
        private static ResourceManager instance;
        public static ResourceManager I => instance ??= new ResourceManager();
        private ResourceManager()
        {
            _passengerResource = new PassengerResource(10000);
            _freightResource = new FreightResource(10000);
        } 
    }
}