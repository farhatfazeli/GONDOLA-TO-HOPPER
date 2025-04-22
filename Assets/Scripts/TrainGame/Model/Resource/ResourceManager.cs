using System;

namespace TrainGame.Model.Resource
{
    public class ResourceManager
    {
        private PassengerResource _passengerResource;
        private FreightResource _freightResource;
        
        public event Action<float> OnPassengerResourceUpdated
        {
            add => _passengerResource.OnValueChanged += value;
            remove => _passengerResource.OnValueChanged -= value;
        }
        
        public event Action<float> OnFreightResourceUpdated
        {
            add => _freightResource.OnValueChanged += value;
            remove => _passengerResource.OnValueChanged -= value;
        }


        private static ResourceManager instance;
        public static ResourceManager I => instance ??= new ResourceManager();
        private ResourceManager()
        {
            _passengerResource = new PassengerResource();
            _freightResource = new FreightResource();
        } 
    }
}