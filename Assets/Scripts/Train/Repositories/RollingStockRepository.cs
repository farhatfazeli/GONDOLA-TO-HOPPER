using System.Collections.Generic;
using System.Linq;
using Train.Model.RollingStock;

namespace Train.Repositories
{
    public class RollingStockRepository
    {
        private readonly List<RollingStockModel> _rollingStock 
            = new List<RollingStockModel>();

        /// <summary>
        /// Adds one or more rolling stock models to the list.
        /// </summary>
        public void AddRollingStock(IEnumerable<RollingStockModel> rollingStock)
        {
            _rollingStock.AddRange(rollingStock);
        }

        /// <summary>
        /// Returns all rolling stock currently in the repository.
        /// </summary>
        public List<RollingStockModel> GetAllRollingStock()
        {
            // Return the actual list reference OR
            // return a new list if you want to avoid external modifications:
            return _rollingStock.ToList(); 
        }

        /// <summary>
        /// Clears all rolling stock.
        /// </summary>
        public void Clear()
        {
            _rollingStock.Clear();
        }
        
        private static RollingStockRepository instance;
        public static RollingStockRepository I => instance ??= new RollingStockRepository();
        private RollingStockRepository()
        {
        }
    }
}