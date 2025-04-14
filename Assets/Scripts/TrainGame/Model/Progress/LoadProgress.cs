using System;
using UnityEngine;

namespace TrainGame.Model.Progress
{
    public enum LoadMode
    {
        Loading,
        Unloading,
    }
    
    public class LoadProgress : IProgressTarget
    {
        public float Current { get; private set; }
        public float Max { get; }

        private readonly LoadMode _loadMode;

        /// <summary>
        /// Constructs a new Load instance with a specified maximum capacity.
        /// </summary>
        /// <param name="maxLoad">The maximum load capacity. Must be greater than or equal to zero.</param>
        /// <param name="loadMode">If the mode is loading or unloading. </param>
        public LoadProgress(float maxLoad, LoadMode loadMode)
        {
            if (maxLoad < 0)
                throw new ArgumentException("Max load must be greater than or equal to zero.", nameof(maxLoad));

            Max = maxLoad;
            Current = 0f;
            _loadMode = loadMode;
        }
        
        public void UpdateProgress(float amount)
        {
            HandleLoading(amount);
        }

        /// <summary>
        /// Increases the current load by a given amount.
        /// </summary>
        public void HandleLoading(float amount)
        {
            switch (_loadMode)
            {
                case LoadMode.Loading:
                    Set(Current + amount);
                    break;
                case LoadMode.Unloading:
                    Set(Current - amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        /// <summary>
        /// Sets the current load to a specified value, clamped between 0 and MaxLoad.
        /// </summary>
        private void Set(float load)
        {
            Current = Mathf.Clamp(load, 0f, Max);
        }
    }
}