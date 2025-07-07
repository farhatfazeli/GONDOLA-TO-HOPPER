using System;
using Core.Persistence;
using Core.Utility;
using UnityEngine;

namespace TrainGame
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        public AudioSource mainLoop;
        public AudioSource trainWhistle;
        public AudioSource trainCrossing;
        public AudioSource travelTheme;
        public AudioSource travelFlute;

        public void PlayTrainWhistle()
        {
            trainWhistle.Play();
        }

        public void PlayTrainCrossing()
        {
            trainCrossing.Play();
        }

        public void PlayTravelTheme()
        {
            mainLoop.Stop();
            travelTheme.Play();
            travelFlute.Play();
        }
    }
}