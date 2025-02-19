using UnityEngine;

namespace Train.View.WorldView
{
    public class RollingStockView : MonoBehaviour
    {
        [SerializeField] private Transform frontCoupler;
        [SerializeField] private Transform rearCoupler;

        public void AlignRollingStock(Transform targetRollingStock)
        {
            Transform targetCoupler = targetRollingStock.GetComponent<RollingStockView>().rearCoupler;

            float targetX = targetCoupler.position.x;
        
            float deltaX = targetX - frontCoupler.position.x;
        
            transform.position += new Vector3(deltaX, 0, 0);
        }
    }
}
