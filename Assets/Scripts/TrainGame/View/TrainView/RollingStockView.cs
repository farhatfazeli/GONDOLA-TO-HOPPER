using System;
using System.Collections.Generic;
using TrainGame.Model.RollingStock;
using UnityEngine;

namespace TrainGame.View.TrainView
{
    public class RollingStockView : MonoBehaviour
    {
        public Transform frontCoupler;
        public Transform rearCoupler;

        public List<Transform> wheels;
        
        private RollingStockModel _rollingStockModel;

        private float _currentPosition;
        private float _previousPosition;
        
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

        private void Update()
        {
            _currentPosition = transform.position.x;
            RotateWheels(_currentPosition - _previousPosition);
            _previousPosition = _currentPosition;
        }

        private void RotateWheels(float distance)
        {
            foreach (Transform wheel in wheels)
            {
                RotateWheel(wheel, distance);
            }
        }

        private void RotateWheel(Transform wheelTransform, float distance)
        {
            float angle = distance * 360 / (2 * Mathf.PI * wheelTransform.position.y);
            wheelTransform.rotation = Quaternion.Euler(0, 0, -angle) * wheelTransform.rotation;
        }
    }
}
