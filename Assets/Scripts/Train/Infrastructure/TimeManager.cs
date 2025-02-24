using System;
using UnityEngine;

namespace Train.Infrastructure
{
    public class TimeManager
    {
        // In-game time in seconds.
        public long CurrentGameTime { get; private set; }
        private float _frameAccumulator = 0f;
        
        public event Action<long> OnTimeChanged;
        
        public void UpdateTime(float deltaRealTime)
        {
            _frameAccumulator += deltaRealTime * SO_GameParameters.I.gameSpeedUpFactor;

            // Convert to long once we pass at least 1 second
            while (_frameAccumulator >= 1f)
            {
                CurrentGameTime += 1;
                _frameAccumulator -= 1f;
            }

            OnTimeChanged?.Invoke(CurrentGameTime);
        }
        
        public void SetCurrentGameTime(long value)
        {
            CurrentGameTime = value;
            OnTimeChanged?.Invoke(CurrentGameTime);
        }
        
        private static TimeManager instance;
        public static TimeManager I => instance ??= new TimeManager();
        private TimeManager()
        {
        }
    }
}