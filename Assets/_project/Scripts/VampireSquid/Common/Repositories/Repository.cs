using System;
using System.Collections.Generic;
using UnityEngine;

namespace VampireSquid.Common.Repositories
{
    [Serializable]
    public abstract class Repository<T> : IRepository<T>
    {
        protected virtual bool OnlyUnique => false;

        private List<T>          items = new();
        public  IReadOnlyList<T> Items => items;

        public void Add(T item)
        {
            if (OnlyUnique && items.Contains(item))
                Debug.Log(NotFoundException(item));
            else
            {
                items.Add(item);
                OnAdded(item);
            }
        }

        public void Remove(T item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                OnRemoved(item);
            }
            else Debug.Log(NotFoundException(item));
        }

        protected virtual void OnAdded(T   item) { }
        protected virtual void OnRemoved(T item) { }

        protected virtual string NotFoundException(T  item) => $"item [{ItemName(item)}] not registered";
        protected virtual string NotUniqueException(T item) => $"item [{ItemName(item)}] already registered";
        protected virtual string ItemName(T           item) => $"{item.ToString()}";
    }
}