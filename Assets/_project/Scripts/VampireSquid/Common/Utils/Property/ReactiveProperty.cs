using System;
using System.Collections.Generic;
using UnityEngine;

namespace VampireSquid.Common.Utils
{
    public interface IReactiveProperty<T>
    {
        T Value { get; set; }

        event Action<T> ValueChanged;
    }

    public interface IReadOnlyReactiveProperty<T>
    {
        T Value { get; }

        event Action<T> ValueChanged;
    }

    [Serializable]
    public class ReactiveProperty<T> : IReactiveProperty<T>, IReadOnlyReactiveProperty<T> 
    {
        [SerializeField] protected T _value = default;

        public ReactiveProperty(T value = default) => Value = value;

        public virtual T Value
        {
            get => _value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    _value = value;
                    OnValueChange();
                }
            }
        }

        public event Action<T> ValueChanged = delegate { };

        protected virtual void OnValueChange() => ValueChanged.Invoke(Value);
    }
}