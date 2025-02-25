using TrainGame.View.TrainView;
using UnityEngine;

public class ShowTrain : MonoBehaviour
{
    public TrainDropdownManager trainDropdownManager;
    public Transform spawnTransform;
    
    public void OnButtonClick()
    {
        TrainConsistViewFactory trainConsistViewFactory = gameObject.AddComponent<TrainConsistViewFactory>();
        trainConsistViewFactory.CreateTrainConsistView(trainDropdownManager.GetSelectedTrain(), spawnTransform);
        Destroy(trainConsistViewFactory);
    }
}
