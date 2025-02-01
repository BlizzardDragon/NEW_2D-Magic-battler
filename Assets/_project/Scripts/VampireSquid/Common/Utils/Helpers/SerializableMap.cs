using System;
using UnityEngine;

namespace VampireSquid.Common.Utils.Helpers
{
    [Serializable]
    public struct SerializableMap<TKey, TValue>
    {
        [field: SerializeField] public TKey   Key   { get; private set; }
        [field: SerializeField] public TValue Value { get; private set; }
    }
}