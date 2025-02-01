using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VampireSquid.Common.BeautifulLogs;
using VampireSquid.Common.DependencyContainer;
using Object = UnityEngine.Object;

namespace VampireSquid.Common.CompositeRoot
{
    public abstract class CompositionBase : IDisposable
    {
        public async virtual UniTask InstallBindings() { }

        public async virtual UniTask PreInitialize() { }

        public async virtual UniTask Initialize() { }

        public virtual void OnBeforeDisposed() { }

        #region GET & FIND

        [HideInCallstack]
        protected TDependency GetLocal<TDependency>() =>
            LocalContext.Get<TDependency>();

        [HideInCallstack]
        protected TDependency GetGlobal<TDependency>() =>
            GlobalContext.Get<TDependency>();

        [HideInCallstack]
        protected TDependency FindMono<TDependency>(bool includeInactive = false) where TDependency : MonoBehaviour
        {
            var find = Object.FindAnyObjectByType<TDependency>(includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude);
            if (find == null) BLog.LogImportant($"Failed to find object of type {typeof(TDependency).Name}");
            return find;
        }

        [HideInCallstack]
        protected TDependency Get<TDependency>()
        {
            if (LocalContext.Contains<TDependency>())
                return LocalContext.Get<TDependency>();
            
            return GlobalContext.Get<TDependency>();
        }

        #endregion

        #region GLOBAL BIND

        protected TDependency CreateAndBindAsGlobal<TDependency>() where TDependency : class
        {
            var instance = DependencyInjector.CreateInstanceWithInject<TDependency>()
                           ?? throw new InvalidOperationException(
                               $"Failed to create instance of {typeof(TDependency)}");

            return BindAsGlobal<TDependency>(instance);
        }

        protected TDependencyInterface CreateAndBindAsGlobal<TDependency, TDependencyInterface>()
            where TDependency : class
        {
            var instance = DependencyInjector.CreateInstanceWithInject<TDependency>()
                           ?? throw new InvalidOperationException(
                               $"Failed to create instance of {typeof(TDependency)}");

            if (instance is not TDependencyInterface dependency)
                throw new InvalidOperationException(
                    $"Dependency {typeof(TDependency)} missing interface {typeof(TDependencyInterface)}");

            return BindAsGlobal<TDependencyInterface>(dependency);
        }

        protected TDependency BindAsGlobal<TDependency>(TDependency dependency)
        {
            if (GlobalContext.Contains<TDependency>())
                return GlobalContext.Get<TDependency>();

            return GlobalContext.Register(dependency);
        }

        #endregion

        #region BIND LOCAL

        protected TDependencyInterface CreateAndBindAsLocal<TDependency, TDependencyInterface>() where TDependency : class
        {
            var instance = DependencyInjector.CreateInstanceWithInject<TDependency>()
                           ?? throw new InvalidOperationException($"Failed to create instance of {typeof(TDependency)}");

            if (instance is not TDependencyInterface dependency)
                throw new InvalidOperationException($"Dependency {typeof(TDependency)} missing interface {typeof(TDependencyInterface)}");

            return BindAsLocal<TDependencyInterface>(dependency);
        }

        protected TDependency CreateAndBindAsLocal<TDependency>() where TDependency : class
        {
            var instance = DependencyInjector.CreateInstanceWithInject<TDependency>()
                           ?? throw new InvalidOperationException($"Failed to create instance of {typeof(TDependency)}");

            return BindAsLocal<TDependency>(instance);
        }

        protected TDependency BindAsLocal<TDependency>(TDependency dependency)
        {
            if (GlobalContext.Contains<TDependency>())
                return GlobalContext.Get<TDependency>();

            if (LocalContext.Contains<TDependency>())
                return LocalContext.Get<TDependency>();

            var registeredDependency = LocalContext.Register(dependency);
            return registeredDependency;
        }
        #endregion

        public void Dispose() => OnBeforeDisposed();
    }
}