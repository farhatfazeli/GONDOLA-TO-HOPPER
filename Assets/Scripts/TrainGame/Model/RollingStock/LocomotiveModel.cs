using ScriptableObjects;

namespace TrainGame.Model.RollingStock
{
    public class LocomotiveModel : RollingStockModel
    {
        public override RollingStockType Type => RollingStockType.Locomotive;

        public float maxSpeed;
        public float tractionCoefficient;
        public float brakingCoefficient;

        public LocomotiveModel(SO_Locomotive locomotive) : this(locomotive.uuid, locomotive.Name, locomotive.mass,
            locomotive.startingAmount, locomotive.startingAmount, locomotive.purchaseCost, locomotive.maxSpeed,
            locomotive.tractionCoefficient, locomotive.brakingCoefficient)
        {
        }

        public LocomotiveModel(string uuid, string name, int mass, int availableAmount, int fleetAmount,
            int purchaseCost, float maxSpeed, float tractionCoefficient, float brakingCoefficient)
        {
            this.uuid = uuid;
            this.name = name;
            this.mass = mass;
            this.AvailableAmount = availableAmount;
            this.FleetAmount = fleetAmount;
            this.purchaseCost = purchaseCost;
            this.maxSpeed = maxSpeed;
            this.tractionCoefficient = tractionCoefficient;
            this.brakingCoefficient = brakingCoefficient;
        }
    }
}