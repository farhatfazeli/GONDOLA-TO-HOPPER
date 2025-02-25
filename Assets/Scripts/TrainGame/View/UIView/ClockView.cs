using TrainGame.Infrastructure;
using UnityEngine;

namespace TrainGame.View.UIView
{
    public class ClockView : MonoBehaviour
    {
        [SerializeField] private RectTransform hourHand;
        [SerializeField] private RectTransform minuteHand;

        private TimeManager _timeManager;
        
        private void OnEnable()
        {
            _timeManager = TimeManager.I;
            _timeManager.OnTimeChanged += UpdateClock;
            
            if (hourHand == null || minuteHand == null)
            {
                throw new System.Exception("ClockView: Missing hourHand or minuteHand reference.");
            }
        }

        private void OnDisable()
        {
            _timeManager.OnTimeChanged -= UpdateClock;
        }

        private void UpdateClock(long gameTimeInSeconds)
        {
            // Convert total game seconds into a day/time-of-day.
            long secondsInDay = gameTimeInSeconds % 86400; // 24 * 60 * 60
            int hours = (int)(secondsInDay / 3600);
            int minutes = (int)((secondsInDay % 3600) / 60);
            int seconds = (int)(secondsInDay % 60);

            // 12-hour clock calculations:
            float hourAngle   = ((hours % 12) + minutes / 60f + seconds / 3600f) * 30f; // 360 deg / 12 hours = 30 deg/hr
            float minuteAngle = (minutes + seconds / 60f) * 6f;  // 360 deg / 60 = 6 deg/min

            hourHand.localRotation   = Quaternion.Euler(0f, 0f, -hourAngle);
            minuteHand.localRotation = Quaternion.Euler(0f, 0f, -minuteAngle);
        }
    }

}