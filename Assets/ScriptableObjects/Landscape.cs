using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLandscape", menuName = "TrainGame/Landscape")]
    public class Landscape : ScriptableObject
    {
        public Sprite image;

        public float trackDepth;
    }
}