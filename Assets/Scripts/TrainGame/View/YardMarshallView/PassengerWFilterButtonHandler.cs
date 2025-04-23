using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.YardMarshallView
{
    public class PassengerWFilterButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button passengerWFilterButton;
        [SerializeField] private YardMarshallController ymc;
        private void Start()
        {
            passengerWFilterButton.onClick.AddListener(() =>
            {
                ymc.PopulateView(YardMarshallFilter.PassengerWagons);
            });
        }
    }
}