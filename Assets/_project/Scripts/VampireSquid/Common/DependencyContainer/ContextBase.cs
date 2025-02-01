using System;
using System.Collections.Generic;
using UnityEngine;
using VampireSquid.Common.BeautifulLogs;

namespace VampireSquid.Common.DependencyContainer
{
    public abstract class ContextBase<T> where T : ContextBase<T>, new()
    {
        private readonly Dictionary<Type, object> dependencies = new();

        private static T _instance;
        public static  T Instance            => _instance ??= new T();
        public static  T CreateNewInstance() => _instance = new T();

        [HideInCallstack]
        public static TDependency Register<TDependency>(TDependency dependency)
        {
            if (Instance.dependencies.TryGetValue(typeof(TDependency), out var service))
                BLog.LogImportant($"Duplicate dependency [{typeof(TDependency).Name}]", typeof(T));

            BLog.Log($"{typeof(TDependency).Name} registered", true, typeof(T));
            Instance.dependencies.Add(typeof(TDependency), dependency);
            return dependency;
        }

        public static bool Contains(Type type) => Instance.dependencies.ContainsKey(type);

        public static bool Contains<TDependency>() => Instance.dependencies.ContainsKey(typeof(TDependency));

        [HideInCallstack]
        public static object Get(Type type)
        {
            if (!Instance.dependencies.TryGetValue(type, out var service))
                BLog.LogImportant($"{type.Name} is missing", typeof(T));

            return service;
        }


        [HideInCallstack]
        public static TDependency Get<TDependency>()
        {
            if (!Instance.dependencies.TryGetValue(typeof(TDependency), out var service))
                BLog.LogImportant($"{typeof(TDependency).Name} is missing", typeof(T));

            return (TDependency)service;
        }

        public void Clear() => dependencies.Clear();
    }
}