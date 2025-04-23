using System;

namespace TrainGame.Model.Resource
{
    public enum ResourceType
    {
        Passengers,
        Freight
    }
    
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

        public void GainResource(int value, ResourceType resourceType)
        {
            switch (value)
            {
                case < 0:
                    throw new ArithmeticException();
                case 0:
                    return;
                default:
                    switch (resourceType)
                    {
                        case ResourceType.Passengers:
                            _passengerResource.value += value;
                            break;
                        case ResourceType.Freight:
                            _freightResource.value += value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
                    }
                    break;
            }
        }

        public void SpendResource(int value, ResourceType resourceType)
        {
            switch (value)
            {
                case < 0:
                    throw new ArithmeticException();
                case 0:
                    return;
                default:
                    switch (resourceType)
                    {
                        case ResourceType.Passengers:
                            _passengerResource.value -= value;
                            break;
                        case ResourceType.Freight:
                            _freightResource.value -= value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
                    }
                    break;
            }
        }

        public bool CheckResourceSpend(int value, ResourceType resourceType)
        {
            switch (value)            
            {
                case < 0:
                    throw new ArithmeticException();
                case 0:
                    return true;
                default:
                    return resourceType switch
                    {
                        ResourceType.Passengers => _passengerResource.value >= value,
                        ResourceType.Freight => _freightResource.value >= value,
                        _ => throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null)
                    };
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