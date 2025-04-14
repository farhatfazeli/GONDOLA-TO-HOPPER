using UnityEngine;

namespace TrainGame.Model.Progress
{
    public class ConstructionProgress : IProgressTarget
    {
        public float Current { get; private set; }
        public float Max { get; }
        public void UpdateProgress(float amount)
        {
            Current = Mathf.Clamp(Current + amount, 0f, Max);
        }
        
        public ConstructionProgress(float maxBuildPoints)
        {
            if (maxBuildPoints < 0)
                throw new System.ArgumentException("Max construction progress must be greater than or equal to zero.", nameof(maxBuildPoints));
            Max = maxBuildPoints;
            Current = 0f;
        }
    }
}