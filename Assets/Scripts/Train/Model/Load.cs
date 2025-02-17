using System;
using UnityEngine;

namespace Train.Model
{
    public enum LoadMode
    {
        Loading,
        Unloading,
        NotSet
    }
    
    public class Load : IProgressTarget
    {
        public float Current { get; private set; }
        public float Max { get; }

        private LoadMode _loadMode;
        
        /// <summary>
        /// Constructs a new Load instance with a specified maximum capacity.
        /// </summary>
        /// <param name="endDistance">The maximum load capacity. Must be greater than zero.</param>
        public Load(float endDistance)
        {
            if (endDistance <= 0)
                throw new ArgumentException("Max load must be greater than zero.", nameof(endDistance));

            Max = endDistance;
            Current = 0f;
            _loadMode = LoadMode.NotSet;
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
                case LoadMode.NotSet:
                    throw new InvalidOperationException("Load mode must be set before loading or unloading.");
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
        
        public void ChangeLoadMode(LoadMode mode)
        {
            _loadMode = mode;
        }
    }
}