using TrainGame.View.TrainView;
using UnityEngine;
using UnityEngine.Serialization;

public class ShowTrain : MonoBehaviour
{
    [FormerlySerializedAs("trainDropdownManager")] public TrainDropdownHandler trainDropdownHandler;
    public Transform spawnTransform;
    
    public void OnButtonClick()
    {
        TrainConsistViewFactory trainConsistViewFactory = gameObject.AddComponent<TrainConsistViewFactory>();
        trainConsistViewFactory.CreateTrainConsistView(trainDropdownHandler.GetSelectedTrain(), spawnTransform);
        Destroy(trainConsistViewFactory);
    }
}
