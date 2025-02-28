using ScriptableObjects;

namespace TrainGame.Model.RollingStock
{
    public class WagonModel : RollingStockModel
    {
        public override RollingStockType Type => RollingStockType.Wagon;

        public LoadType loadType;
        
        public WagonModel(SO_Wagon wagon) : this(wagon.uuid, wagon.name, wagon.mass, wagon.startingAmount,
            wagon.startingAmount, wagon.purchaseCost, wagon.loadType)
        {
        }
        
        public WagonModel(string uuid, string name, int mass, int availableAmount, int fleetAmount, int purchaseCost,
            LoadType loadType)
        {
            this.uuid = uuid;
            this.name = name;
            this.mass = mass;
            this.AvailableAmount = availableAmount;
            this.FleetAmount = fleetAmount;
            this.purchaseCost = purchaseCost;
            this.loadType = loadType;
        }
    }
}