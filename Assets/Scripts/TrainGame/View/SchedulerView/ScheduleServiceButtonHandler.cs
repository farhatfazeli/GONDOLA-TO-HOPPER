using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.SchedulerView
{
    public class ScheduleServiceButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button scheduleServiceButton;
        [SerializeField] private ServiceDocketController sdc;
        private void Start()
        {
            scheduleServiceButton.onClick.AddListener(() =>
            {
                sdc.ScheduleService();
            });
        }
    }
}