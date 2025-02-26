using UnityEngine;

namespace TrainGame.View.UIView
{
    public class TabHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform activeTab;
        [SerializeField] private RectTransform inactiveTab;
        
        public void OnTabClick()
        {
            activeTab.gameObject.SetActive(true);
            inactiveTab.gameObject.SetActive(false);
        }
        
        public void Deactivate()
        {
            activeTab.gameObject.SetActive(false);
            inactiveTab.gameObject.SetActive(true);
        }
    }
}