using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.YardMarshallView
{
    public class FreightWFilterButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button freightWFilterButton;
        [SerializeField] private YardMarshallController ymc;
        private void Start()
        {
            freightWFilterButton.onClick.AddListener(() =>
            {
                ymc.PopulateView(YardMarshallFilter.FreightWagons);
            });
        }
    }
}