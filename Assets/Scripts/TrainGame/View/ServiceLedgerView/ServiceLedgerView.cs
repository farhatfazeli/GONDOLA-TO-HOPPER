using TrainGame.Model.Service;
using UnityEngine;

namespace TrainGame.View.ServiceLedgerView
{
    public class ServiceLedgerView : MonoBehaviour
    {
        [SerializeField] private GameObject serviceViewItemPrefab;
        [SerializeField] private RectTransform contentContainer;
        
        public void AddServiceViewItem(ServiceModel serviceModel)
        {
            GameObject go = Instantiate(serviceViewItemPrefab, contentContainer);
            go.GetComponent<ServiceLedgerItemView>().Initialize(serviceModel);
        }
    }
}