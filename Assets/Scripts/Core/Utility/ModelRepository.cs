using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Utility
{
    public class ModelRepository<TModel> where TModel : IIdentifiable, IModelObservable
    {
        protected readonly HashSet<TModel> _list  = new HashSet<TModel>();
        
        protected event Action OnListUpdated;
        
        public event Action ListUpdated
        {
            add => OnListUpdated += value;
            remove => OnListUpdated -= value;
        }

        public void Add(TModel model)
        {
            TryAdd(model);
            InvokeOnListUpdated();
        }

        public void AddRange(IEnumerable<TModel> models)
        {
            if(models == null)
                throw new ArgumentNullException(nameof(models));

            bool hasNewModels = false;
            
            foreach (TModel model in models)
            {
                if(TryAdd(model))
                    hasNewModels = true;
            }
            
            if (hasNewModels) InvokeOnListUpdated();
        }

        private bool TryAdd(TModel model)
        {
            ThrowIfNull(model);

            if (!_list.Add(model)) return false;
            
            model.OnModelChanged += InvokeOnListUpdated;

            return true;
        }

        public void Remove(TModel model)
        {
            ThrowIfNull(model);
            model.OnModelChanged -= InvokeOnListUpdated;
            _list.Remove(model);
            InvokeOnListUpdated();
        }
        
        public void Clear()
        {
            _list.Clear();
            InvokeOnListUpdated();
        }

        private void InvokeOnListUpdated()
        {
            OnListUpdated?.Invoke();
        }
        
        private static void ThrowIfNull(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
        }
    }
}