using Train.Model.RollingStock;
using UnityEngine;

namespace Train.View.WorldView
{
    public class RollingStockView : MonoBehaviour
    {
        public Transform frontCoupler;
        public Transform rearCoupler;
        
        private RollingStockModel _rollingStockModel;
        
        public void Initialize(RollingStockModel rollingStockModel)
        {
            _rollingStockModel = rollingStockModel;
        }

        public void AlignRollingStock(Transform targetRollingStock)
        {
            Transform targetCoupler = targetRollingStock.GetComponent<RollingStockView>().rearCoupler;

            float targetX = targetCoupler.position.x;
        
            float deltaX = targetX - frontCoupler.position.x;
        
            transform.position += new Vector3(deltaX, 0, 0);
        }
    }
}
