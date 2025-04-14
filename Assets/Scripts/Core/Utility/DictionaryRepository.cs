using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Utility
{
    public class DictionaryRepository<TModel, TSo> where TModel : IIdentifiable, IModelObservable
    {
        private Dictionary<TModel, TSo> _lookup;

        public event Action OnRepositoryUpdated;

        public virtual void Initialize(List<TModel> models, List<TSo> sos)
        {
            if (models.Count != sos.Count)
                throw new ArgumentException("The number of models and scriptable objects must match.");

            _lookup = new Dictionary<TModel, TSo>();
            
            for (int i = 0; i < models.Count; i++)
            {
                _lookup.Add(models[i], sos[i]);
                models[i].OnModelChanged += OnRepositoryUpdated;
            }
        }

        public virtual TSo GetSo(TModel model)
        {
            if (_lookup == null)
                throw new InvalidOperationException("Repository not initialized. Call Initialize() first.");

            return _lookup[model];
        }

        public virtual TModel GetModel(TSo so)
        {
            if (_lookup == null)
                throw new InvalidOperationException("Repository not initialized. Call Initialize() first.");

            foreach (var pair in _lookup)
            {
                if (EqualityComparer<TSo>.Default.Equals(pair.Value, so))
                    return pair.Key;
            }

            throw new KeyNotFoundException("No model found for the given ScriptableObject.");
        }

        public virtual IEnumerable<TModel> GetModels()
        {
            return _lookup.Keys;
        }

        public TModel GetByUuid(string uuid)
        {
            return _lookup.Keys.FirstOrDefault(x => x.uuid == uuid);
        }

        public void Clear()
        {
            _lookup.Clear();
        }
    }
    
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