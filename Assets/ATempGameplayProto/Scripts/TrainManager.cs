using System.Collections.Generic;
using UnityEngine;

public class TrainManager : PersistentSingleton<TrainManager>
{
    public List<TrainObject> trains = new List<TrainObject>();
}
