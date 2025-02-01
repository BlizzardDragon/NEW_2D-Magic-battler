using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameKit.Dependencies.Utilities;
using JetBrains.Annotations;
using UnityEngine;
using VampireSquid.Common.BeautifulLogs;

namespace VampireSquid.Common.DependencyContainer
{
    public class DependencyInjector
    {
        private static readonly BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
        private static readonly Dictionary<Type, object> dependencyCache = new();
        private static readonly Dictionary<Type, List<MethodInfo>> methodCache = new();

        public static T CreateInstanceWithInject<T>() where T : class
        {
            var instanceType = typeof(T);
            var constructor = GetSingleConstructor(instanceType);
            var args = GetArgs(constructor.GetParameters());

            var instance = (T)constructor.Invoke(args);
            Inject(instance);
            return instance;
        }

        private static ConstructorInfo GetSingleConstructor(Type instanceType)
        {
            var constructors = instanceType.GetConstructors(bindingFlags);

            if (constructors.Length != 1)
                throw new InvalidOperationException($"Type {instanceType.Name} must have exactly one constructor.");

            return constructors[0];
        }

        public static void Inject(object instance)
        {
            try
            {
                var type = instance.GetType();

                if (!methodCache.TryGetValue(type, out var methods))
                {
                    methods = type.GetMethods(bindingFlags).Where(m => m.IsDefined(typeof(InjectAttribute))).ToList();
                    methodCache[type] = methods;
                }

                foreach (var method in methods) 
                    InvokeMethod(method, instance);
            }
            catch (Exception e)
            {
                BLog.LogImportant(e.Message, typeof(object));
            }
        }

        public static void Inject(GameObject gameObject, bool includeChilds = false)
        {
            var monoBehaviours = gameObject.GetComponents<MonoBehaviour>();

            foreach (var monoBehaviour in monoBehaviours)
                Inject(monoBehaviour);

            if (includeChilds)
            {
                foreach (Transform item in gameObject.transform)
                    Inject(item.gameObject, true);
            }
        }

        private static void InvokeMethod(MethodInfo method, object target) =>
            method.Invoke(target, GetArgs(method.GetParameters()));

        private static object[] GetArgs(ParameterInfo[] parameters)
        {
            var args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var type = parameters[i].ParameterType;
                object dependency = GetDependency(type);

                if (dependency != null)
                    args[i] = dependency;
                else if (parameters[i].CustomAttributes.Any(x => x.AttributeType == typeof(CanBeNullAttribute)))
                    args[i] = null;
                else
                {
                    var parentType = parameters[i].Member.DeclaringType.Name;
                    
                    throw new InvalidOperationException($"Missing {type.Name} for injection in {parentType}");
                }
            }

            return args;
        }

        private static object GetDependency(Type type)
        {
            if (dependencyCache.TryGetValue(type, out object cachedDependency))
                return cachedDependency;

            if (GlobalContext.Contains(type))
            {
                dependencyCache[type] = GlobalContext.Get(type);
                return dependencyCache[type];
            }

            if (LocalContext.Contains(type))
            {
                dependencyCache[type] = LocalContext.Get(type);
                return dependencyCache[type];
            }

            return null;
        }
    }
}
