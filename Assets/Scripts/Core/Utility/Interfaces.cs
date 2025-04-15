using System;

namespace Core.Utility
{
    public interface IIdentifiable
    {
        string uuid { get; }
        string name { get; }
    }

    public interface IModelObservable
    {
        event Action OnModelChanged;
    }
}