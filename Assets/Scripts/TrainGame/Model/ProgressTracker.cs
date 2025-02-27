using System;
using ScriptableObjects;
using UnityEngine;

namespace TrainGame.Model
{
    public interface IProgressTarget
    {
        float Current { get; }
        float Max { get; }

        void UpdateProgress(float amount);
    }

    public class ProgressTracker
    {
        public bool IsFinished => CurrentProgress >= TargetProgress;
        public float Progress => Mathf.Clamp01(CurrentProgress / TargetProgress);

        public float TimeRemaining => IsFinished
            ? 0
            : (TargetProgress - CurrentProgress) / (_progressRate * SO_GameParameters.I.gameSpeedUpFactor);

    
        public event Action OnProgressComplete;
    
    
        private readonly IProgressTarget _progressTarget;
        private float TargetProgress => _progressTarget.Max;
        private float CurrentProgress => _progressTarget.Current;

        private readonly float _progressRate;
        private bool _isStarted;
        private bool _isPaused;

        public void Start()
        {
            _isStarted = true;
        }
        
        public void Finish()
        {
            _progressTarget.UpdateProgress(TargetProgress);
            OnProgressComplete?.Invoke();
        }

        /// <summary>
        /// Constructs a ProgressTracker that uses an IProgressable instance.
        /// </summary>
        public ProgressTracker(IProgressTarget progressTarget, float progressRate)
        {
            _progressTarget = progressTarget;
            _progressRate = progressRate > 0 ? progressRate : 1;
        }

        /// <summary>
        /// Advances progress based on deltaTime.
        /// </summary>
        public void Advance(float deltaTime)
        {
            if (_isPaused || !_isStarted) return;
            AdvanceBy(_progressRate * SO_GameParameters.I.gameSpeedUpFactor * deltaTime);
        }

        /// <summary>
        /// Advances progress by a specified amount.
        /// </summary>
        public void AdvanceBy(float amount)
        {
            if(IsFinished) return;
            _progressTarget.UpdateProgress(amount);
            if (IsFinished) OnProgressComplete?.Invoke();
        }

        public void AdvancePercentage(float progress)
        {
            AdvanceBy(progress * TargetProgress);
        }
    
        public void Pause() => _isPaused = true;

        public void Resume() => _isPaused = false;
    }
}