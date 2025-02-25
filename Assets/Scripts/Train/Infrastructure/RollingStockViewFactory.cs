using System.Collections.Generic;
using Train.Model.RollingStock;
using UnityEngine;

namespace Train.Infrastructure
{
    public class RollingStockViewFactory
    {
        
        
        private static RollingStockViewFactory instance;
        public static RollingStockViewFactory I => instance ??= new RollingStockViewFactory();
        private RollingStockViewFactory()
        {
        }
    }
}