using System;
using TrainGame.Model.TrainConsist;
using UnityEngine;

namespace TrainGame.Controller
{
    public class SchedulerController : MonoBehaviour
    {
        private TrainConsistManager _trainConsistManager;
        private void Awake()
        {
            _trainConsistManager = TrainConsistManager.I;
        }
    }
}