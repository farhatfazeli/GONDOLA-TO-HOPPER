using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.CustomEventSystem
{
    [Serializable]
    public class CustomResponse : UnityEvent<Component, object>
    {
    }
}