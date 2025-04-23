using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.YardMarshallView
{
    public class ResetTrainConsistButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button resetTrainConsistButton;
        [SerializeField] private YardMarshallController ymc;
        private void Start()
        {
            resetTrainConsistButton.onClick.AddListener(() =>
            {
                ymc.ResetTrainConsist();
            });
        }
    }
}