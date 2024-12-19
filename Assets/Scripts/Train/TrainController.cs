using Train.Model;
using UnityEngine;

namespace Train
{
    public class TrainController : MonoBehaviour
    {
        private TrainModel _trainModel;
        public void DispatchTrain()
        {
            //_trainEngine.Start();
            Debug.Log("Train dispatched");
        }

        public void ShowView()
        {
            Debug.Log("Train view shown");
        }

        public void DestroyView()
        {
            Debug.Log("Train view destroyed");
        }
    }
}
