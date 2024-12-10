using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWheel", menuName = "TrainGame/Wheel")]
    public class Wheel : ScriptableObject
    {
        public float size = 1f;

        private float GetCircumference()
        {
            return Mathf.PI * size;
        }
        
        public float GetAngularVelocity(float speed)
        {
            return speed / GetCircumference();
        }
    }
}