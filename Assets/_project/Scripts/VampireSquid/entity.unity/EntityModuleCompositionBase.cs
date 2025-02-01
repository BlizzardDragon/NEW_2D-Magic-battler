using System;
using System.Collections.Generic;
using UnityEngine;
using VampireSquid.Common.CompositeRoot;
using VampireSquid.Common.DependencyContainer;

namespace Entity.Core
{
    public abstract class EntityModuleCompositionBase : IDisposable
    {
        private readonly List<IUpdateLoop>      _updatable      = new(100);
        private readonly List<IFixedUpdateLoop> _fixedUpdatable = new(100);
        private readonly List<ILateUpdateLoop>  _lateUpdatable  = new(100);

        public abstract void Create(IEntity entity);

        public virtual void Initialize() { }

        [HideInCallstack]
        public void AddUpdatable(IUpdateLoop updatable)
        {
            UpdateManager.AddUpdatable(updatable);
            _updatable.Add(updatable);
        }

        [HideInCallstack]
        public void AddFixedUpdatable(IFixedUpdateLoop fixedUpdatable)
        {
            UpdateManager.AddFixedUpdatable(fixedUpdatable);
            _fixedUpdatable.Add(fixedUpdatable);
        }

        [HideInCallstack]
        public void AddLateUpdatable(ILateUpdateLoop lateUpdatable)
        {
            UpdateManager.AddLateUpdatable(lateUpdatable);
            _lateUpdatable.Add(lateUpdatable);
        }

        [HideInCallstack]
        protected static TDependency GetLocal<TDependency>() => LocalContext.Get<TDependency>();

        [HideInCallstack]
        protected static TDependency GetGlobal<TDependency>() => GlobalContext.Get<TDependency>();

        [HideInCallstack]
        protected static TDependency Get<TDependency>()
        {
            var type = typeof(TDependency);

            if (GlobalContext.Contains(type))
                return GlobalContext.Get<TDependency>();

            return LocalContext.Get<TDependency>();
        }

        protected virtual void OnBeforeDestroy() { }

        public void Dispose()
        {
            foreach (var updatable in _updatable)
                UpdateManager.RemoveUpdatable(updatable);

            foreach (var fixedUpdatable in _fixedUpdatable)
                UpdateManager.RemoveFixedUpdatable(fixedUpdatable);

            foreach (var lateUpdatable in _lateUpdatable)
                UpdateManager.RemoveLateUpdatable(lateUpdatable);

            OnBeforeDestroy();
        }
    }
}