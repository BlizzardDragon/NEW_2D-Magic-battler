using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VampireSquid.Common.Commands;
using VampireSquid.Common.Commands.Handlers;
using VampireSquid.Common.DependencyContainer;

namespace VampireSquid.Common.CompositeRoot
{
    public enum SceneLoadingType
    {
        Standalone,
        Additive
    }

    [DefaultExecutionOrder(-10000)]
    public class SceneCompositeRoot : MonoBehaviour
    {
        [SerializeField] private SceneLoadingType        loadingType;
        [SerializeField] private List<CompositeRootBase> order;
        [SerializeField] private List<MonoBehaviour>     cachedInjectables;
        [SerializeField] private List<MonoBehaviour>     cachedCommandListeners;

        public bool IsInitialized { get; private set; } = false;
        
        private LocalContext   _localContext;
        private CommandHandler _commandHandler;

        private readonly List<IUpdateLoop>      _updatables      = new();
        private readonly List<IFixedUpdateLoop> _fixedUpdatables = new();
        private readonly List<ILateUpdateLoop>  _lateUpdatables  = new();

        private bool _quitting;

        private void OnValidate()
        {
            FetchCachedObjects();
            FetchCompositeRoots();
        }

        private async void Awake()
        {
            switch (loadingType)
            {
                case SceneLoadingType.Additive:
                    _localContext   = LocalContext.Instance;
                    _commandHandler = CommandHandler.Instance;
                    break;
                default:
                    _localContext   = LocalContext.CreateNewInstance();
                    _commandHandler = CommandHandler.Instance.NewInstance();
                    break;
            }

            foreach (var root in order)
            {
                await root.InstallBindings();
                foreach (var composition in root.Compositions)
                    await composition.InstallBindings();
            }

            InjectDependeciesToGameObjects();
            AddCachedCommandListeners();

            foreach (var root in order)
            {
                await root.PreInitialize();

                foreach (var composition in root.Compositions)
                    await composition.PreInitialize();
            }

            foreach (var root in order)
            {
                await root.Initialize();

                foreach (var composition in root.Compositions)
                    await composition.Initialize();
            }

            foreach (var root in order)
            {
                if (root is IUpdateLoop upd) _updatables.Add(upd);
                foreach (var comp in root.Compositions.OfType<IUpdateLoop>())
                    _updatables.Add(comp);

                if (root is IFixedUpdateLoop fixedUpd) _fixedUpdatables.Add(fixedUpd);
                foreach (var comp in root.Compositions.OfType<IFixedUpdateLoop>())
                    _fixedUpdatables.Add(comp);

                if (root is ILateUpdateLoop lateUpd) _lateUpdatables.Add(lateUpd);
                foreach (var comp in root.Compositions.OfType<ILateUpdateLoop>())
                    _lateUpdatables.Add(comp);
            }
            
            IsInitialized = true;
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            for (int i = 0; i < _updatables.Count; i++)
                _updatables[i].OnUpdate(deltaTime);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;

            for (int i = 0; i < _fixedUpdatables.Count; i++)
                _fixedUpdatables[i].OnFixedUpdate(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;

            for (int i = 0; i < _lateUpdatables.Count; i++)
                _lateUpdatables[i].OnLateUpdate(deltaTime);
        }

        private void OnApplicationQuit()
        {
            _quitting = true;
        }

        private void OnDestroy()
        {
            if (_quitting) return;
            
            foreach (var root in order)
            {
                foreach (var composition in root.Compositions)
                    composition.OnBeforeDisposed();
                root.OnBeforeDestroyed();
            }

            _commandHandler.CleanUp();
            _localContext.Clear();
        }

        private void InjectDependeciesToGameObjects()
        {
            foreach (var cachedGameObject in cachedInjectables)
                DependencyInjector.Inject(cachedGameObject.gameObject);
        }

        private void AddCachedCommandListeners()
        {
            foreach (var cachedGameObject in cachedCommandListeners)
                _commandHandler.AddListener(cachedGameObject);
        }

        private void FetchCompositeRoots()
        {
            order.Clear();
            order = GetComponentsInChildren<CompositeRootBase>().ToList();
        }

        private void FetchCachedObjects()
        {
            cachedInjectables.Clear();
            cachedCommandListeners.Clear();

            var monoBehaviours = FindObjectsOfType<MonoBehaviour>(this);

            foreach (var monoBehaviour in monoBehaviours)
            {
                var type    = monoBehaviour.GetType();
                var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(InjectAttribute), inherit: true);

                    if (attributes.Length > 0 && attributes.Any(p => p is InjectAttribute { AutoFind: true }))
                    {
                        cachedInjectables.Add(monoBehaviour);
                        break;
                    }
                }

                foreach (var implementedInterface in type.GetInterfaces())
                {
                    if (implementedInterface.IsGenericType
                        && implementedInterface.GetGenericTypeDefinition() == typeof(ICommandListener<>))
                    {
                        cachedCommandListeners.Add(monoBehaviour);
                        break;
                    }
                }
            }
        }
    }
}