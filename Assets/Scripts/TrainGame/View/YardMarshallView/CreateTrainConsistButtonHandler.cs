using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.YardMarshallView
{
    public class CreateTrainConsistButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button createTrainConsistButton;
        [SerializeField] private YardMarshallController ymc;
        private void Start()
        {
            createTrainConsistButton.onClick.AddListener(() =>
            {
                ymc.CreateTrainConsist();
            });
        }
    }
}