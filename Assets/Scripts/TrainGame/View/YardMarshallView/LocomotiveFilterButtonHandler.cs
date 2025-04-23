using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.YardMarshallView
{
    public class LocomotiveFilterButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button locomotiveFilterButton;
        [SerializeField] private YardMarshallController ymc;
        private void Start()
        {
            locomotiveFilterButton.onClick.AddListener(() =>
            {
                ymc.PopulateView(YardMarshallFilter.Locomotives);
            });
        }
    }
}