
public class TrainObject
{
    public readonly int uuid;
    public string name;
    public float maxSpeed;
    public float tractionCoefficient;
    public float brakingCoefficient;
    public float mass;

    public TrainObject()
    {
        uuid = System.Guid.NewGuid().GetHashCode();
    }
    
    public TrainObject(int uuid, string name, float maxSpeed, float tractionCoefficient, float brakingCoefficient, float mass)
    {
        this.uuid = uuid;
        this.name = name;
        this.maxSpeed = maxSpeed;
        this.tractionCoefficient = tractionCoefficient;
        this.brakingCoefficient = brakingCoefficient;
        this.mass = mass;
    }

    // public void PopulateSaveData(SaveData sd)
    // {
    //     sd.trainData.Add(this);
    // }
    //
    // public void LoadFromSaveData(SaveData sd)
    // {
    //     foreach (var trainObject in sd.trainData)
    //     {
    //         if (trainObject.uuid == uuid)
    //         {
    //             maxSpeed = trainObject.maxSpeed;
    //             tractionCoefficient = trainObject.tractionCoefficient;
    //             brakingCoefficient = trainObject.brakingCoefficient;
    //             mass = trainObject.mass;
    //         }
    //     }
    // }

    public void Reset()
    {
        
    }
}


// public class TrainObject : ISaveable
// {
//     public string instanceID;
//     public float maxSpeed;
//     public float tractionCoefficient;
//     public float brakingCoefficient;
//     public float mass;
//
//     public TrainObject()
//     {
//         
//     }
//     
//     public TrainObject(float maxSpeed, float tractionCoefficient, float brakingCoefficient, float mass)
//     {
//         instanceID = System.Guid.NewGuid().ToString();
//         this.maxSpeed = maxSpeed;
//         this.tractionCoefficient = tractionCoefficient;
//         this.brakingCoefficient = brakingCoefficient;
//         this.mass = mass;
//     }
//
//     public void PopulateSaveData(SaveData sd)
//     {
//         sd.trainObjects.Add(this);
//     }
//
//     public void LoadFromSaveData(SaveData sd)
//     {
//         foreach (var trainObject in sd.trainObjects)
//         {
//             if (trainObject.instanceID == instanceID)
//             {
//                 maxSpeed = trainObject.maxSpeed;
//                 tractionCoefficient = trainObject.tractionCoefficient;
//                 brakingCoefficient = trainObject.brakingCoefficient;
//                 mass = trainObject.mass;
//             }
//         }
//     }
// }