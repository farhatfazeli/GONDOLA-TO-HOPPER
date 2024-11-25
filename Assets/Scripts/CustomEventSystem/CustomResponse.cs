using System;
using UnityEngine;
using UnityEngine.Events;

namespace CustomEventSystem
{
    [Serializable]
    public class CustomResponse : UnityEvent<Component, object>
    {
        int hello = 5;
    }
}