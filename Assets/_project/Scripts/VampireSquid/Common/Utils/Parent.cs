using System;
using UnityEngine;

namespace VampireSquid.Common.Utils
{
    public class Parent
    {
        public bool Initialized { get; private set; } = false;

        private readonly string name;

        private Transform parent;
        private Transform parentForThisParent;

        private bool initialized = false;

        public Parent(string name, Transform spawnUnder = null, Action<Transform> created = null)
        {
            this.name           = name;
            parentForThisParent = spawnUnder;
            created?.Invoke(Transform);
        }

        public Transform Transform => parent ??= GetNewParent();

        private Transform GetNewParent()
        {
            var newParent = new GameObject(name).transform;
            newParent.SetParent(parentForThisParent);
            parent      = newParent;
            Initialized = true;
            return newParent;
        }

        public void SetParent(Transform p) => Transform.SetParent(p);
        
        public static implicit operator Transform(Parent parent) => parent.Transform;
    }
}