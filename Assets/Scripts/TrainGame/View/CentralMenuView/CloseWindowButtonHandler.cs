using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.CentralMenuView
{
    public class CloseWindowButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        
        private void Start()
        {
            closeButton.onClick.AddListener(() =>
            {
                UIStateManager.I.OnCloseWindowButtonClicked();
            });
        }
    }
}