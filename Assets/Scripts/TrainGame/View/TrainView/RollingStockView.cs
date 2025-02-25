using TrainGame.Model.RollingStock;
using UnityEngine;

namespace TrainGame.View.TrainView
{
    public class RollingStockView : MonoBehaviour
    {
        public Transform frontCoupler;
        public Transform rearCoupler;
        
        private RollingStockModel _rollingStockModel;
        
        public RollingStockView Initialize(RollingStockModel rollingStockModel)
        {
            _rollingStockModel = rollingStockModel;
            return this;
        }

        public RollingStockView AlignRollingStock(Transform targetCoupler)
        {
            float targetX = targetCoupler.position.x;
        
            transform.position += new Vector3(targetX - frontCoupler.position.x, 0, 0);
            
            return this;    
        }
    }
}
