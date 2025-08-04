using UnityEngine;

namespace ATrainGamePrototype.Input
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private TrainUIInputMap _trainUIInputMap;
        
        [SerializeField] private UIController _UIController;
        
        private void Start() {
            EnableUIController();
            //EnableGlobalMap();
        }

        // private void EnableGlobalMap() {
        //     _inputReaderGlobalMap.Pause += OnPause;
        //     _inputReaderGlobalMap.Reset += OnReset;
        //     _inputReaderGlobalMap.EnablePlayerActions();
        // }
        
        private void EnableUIController() {
            DisableControllers();
            _UIController.EnableControllerInput(_trainUIInputMap);
        }

        private void DisableControllers() {
            // _weaponController.DisableControllerInput();
            // _tossingController.DisableControllerInput();
            // _uiController.DisableControllerInput();
        }
    }
}

