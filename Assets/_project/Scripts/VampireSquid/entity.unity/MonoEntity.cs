using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VampireSquid.Common.BeautifulLogs;
using VampireSquid.Common.Connections;

namespace Entity.Core
{
    public class MonoEntity : MonoBehaviour, IEntity
    {
        private readonly Dictionary<Type, object> _modulesMap = new();

        [SerializeField] private List<EntityModuleCompositeRootBase> _moduleCompositeRoots;
        [SerializeField] private bool initializeOnAwake;

        [field: SerializeField] public int Id { get; set; }
        [field: SerializeField] public NetworkedPresence Presence { get; private set; }

        public bool IsInitialized { get; private set; }

        public event Action Initialized;

        private void OnValidate()
        {
            _moduleCompositeRoots = GetComponentsInChildren<EntityModuleCompositeRootBase>().ToList();
        }

        private void Awake()
        {
            if (initializeOnAwake)
                Initialize(Presence);
        }

        public void Initialize(NetworkedPresence networkedPresence)
        {
            if (IsInitialized)
                return;

            Presence = networkedPresence;

            foreach (var factory in _moduleCompositeRoots)
                factory.Create(this);

            foreach (var factory in _moduleCompositeRoots)
            {
                factory.InitializeCompositions();
                factory.Initialize();
            }

            IsInitialized = true;

            Initialized?.Invoke();
            
            Debug.Log($"Entity {gameObject.name} initialized with presence: {Presence}");
        }

        public TModule AddModule<TModule>(TModule module)
        {
            if (_modulesMap.TryGetValue(typeof(TModule), out var service))
                BLog.LogImportant($"{gameObject.name}: Type {typeof(TModule).Name} duplication found.");

            _modulesMap.Add(typeof(TModule), module);

            return module;
        }

        public bool ContainsModule<TModule>() => _modulesMap.ContainsKey(typeof(TModule));

        public TModule GetModule<TModule>()
        {
            if (!_modulesMap.TryGetValue(typeof(TModule), out var service))
                BLog.LogImportant($"{gameObject.name}: Type {typeof(TModule).Name} is not found.");

            return (TModule)service;
        }

        public bool TryGetModule<TModule>(out TModule module)
        {
            module = default;

            if (_modulesMap.TryGetValue(typeof(TModule), out var service))
            {
                module = (TModule)service;
                return true;
            }

            return false;
        }   
    }
}