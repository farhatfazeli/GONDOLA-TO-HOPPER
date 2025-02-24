using System;
using Persistence;

namespace Train.Infrastructure
{
    public abstract class TimeSaveHelper
    {
        public static void PopulateSaveData(SaveData sd)
        {
            sd.savedGameTime = TimeManager.I.CurrentGameTime;
            sd.savedRealTime = GetCurrentSystemTime();
        }

        public static void LoadFromSaveData(SaveData sd)
        {
            long currentGameTime = 0;
            
            long savedGameTime = sd.savedGameTime;
            long savedRealTime = sd.savedRealTime;
            
            // 2. If we have never saved before, just set currentGameTime = 0 or the saved value
            //    If we have saved, then figure out how much real time has passed:
            if (savedRealTime == 0)
            {
                // No previous data; start from zero or from the savedGameTime
                currentGameTime = savedGameTime;
                // Also set the lastRealTime to now so we don't double-calculate later
                savedRealTime = GetCurrentSystemTime();
            }
            else
            {
                // Real time that passed since last session:
                var now = GetCurrentSystemTime();
                var realTimeDiff = now - savedRealTime; // in seconds

                currentGameTime = savedGameTime + (long)(realTimeDiff * SO_GameParameters.I.gameSpeedUpFactor);
            }
            
            // (Optional) Make sure currentGameTime is not negative or some weird edge case:
            if (currentGameTime < 0) currentGameTime = 0;
            
            TimeManager.I.SetCurrentGameTime(currentGameTime);
        }
        
        // ---- Utility Method ----

        // Returns current Unix epoch time in seconds.
        private static long GetCurrentSystemTime()
        {
            // DateTimeOffset.Now.ToUnixTimeSeconds() requires .NET 4.x or .NET Standard 2.0 or above
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}