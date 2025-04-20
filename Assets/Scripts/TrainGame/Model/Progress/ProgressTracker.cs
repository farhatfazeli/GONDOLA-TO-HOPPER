using System;
using ScriptableObjects;
using UnityEngine;

namespace TrainGame.Model.Progress
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
        private float CurrentProgress => _progressTarget.Current;
        private float TargetProgress => _progressTarget.Max;

        private readonly float _progressRate;
        private bool _isStarted;
        private bool _isPaused;

        public void StartAuto()
        {
            _isStarted = true;
        }
        
        public void ForceFinish()
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
            _progressRate = progressRate;
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
        
        public void SetProgress(float progress)
        {
            _progressTarget.UpdateProgress(progress * TargetProgress);
            if (IsFinished) OnProgressComplete?.Invoke();
        }
    
        public void Pause() => _isPaused = true;

        public void Resume() => _isPaused = false;
    }

    // public sealed class Rate : IEquatable<Rate>
    // {
    //     /// <summary>
    //     /// The underlying rate value.
    //     /// </summary>
    //     private readonly float value;
    //
    //     public Rate(float value)
    //     {
    //         if (value < 0)
    //         {
    //             throw new ArgumentOutOfRangeException(nameof(value), value, "Rate value cannot be less than 0");
    //         }
    //         this.value = value;
    //     }
    //     
    //     // Multiply Rate by a float:
    //     public static Rate operator *(Rate rate, float f)
    //         => new Rate(rate.value * f);
    //
    //     // Add two Rates:
    //     public static Rate operator +(Rate a, Rate b)
    //         => new Rate(a.value + b.value);
    //
    //     // support float * Rate:
    //     public static Rate operator *(float f, Rate rate)
    //         => rate * f;
    //     
    //     // support float + Rate:
    //     public static Rate operator +(Rate a, float b)
    //         => new Rate(a.value + b);
    //     
    //     public static Rate Clamp(Rate value, float min, float max)
    //     {
    //         return new Rate(Mathf.Clamp(value.value, min, max));
    //     }
    //
    //     // Implicit conversion back to float for convenience:
    //     public static implicit operator float(Rate r) => r.value;
    //
    //     // Explicit conversion from float to Rate
    //     public static explicit operator Rate(float v) => new Rate(v);
    //
    //     // Equality members so you can compare Rates sensibly:
    //     public override bool Equals(object obj) => Equals(obj as Rate);
    //     public bool Equals(Rate other) => other != null && value.Equals(other.value);
    //     public override int GetHashCode() => value.GetHashCode();
    //     public static bool operator ==(Rate a, Rate b) => Equals(a, b);
    //     public static bool operator !=(Rate a, Rate b) => !Equals(a, b);
    //
    //     public override string ToString() => value.ToString("G4");
    // }

}