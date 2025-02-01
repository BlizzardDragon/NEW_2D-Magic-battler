using Common.Tools;
using System.Collections.Generic;
using UnityEngine;
using VampireSquid.Common.CompositeRoot;

namespace Entity.Core
{
    public class UpdateManager : MonoSingleton<UpdateManager>
    {
        private List<IUpdateLoop> _updatables = new(1000);
        private List<IFixedUpdateLoop> _fixedUpdatables = new(1000);
        private List<ILateUpdateLoop> _lateUpdatables = new(1000);

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            for (int i = 0; i < _updatables.Count; i++)
                _updatables[i].OnUpdate(deltaTime);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.deltaTime;

            for (int i = 0; i < _fixedUpdatables.Count; i++)
                _fixedUpdatables[i].OnFixedUpdate(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;

            for (int i = 0; i < _lateUpdatables.Count; i++)
                _lateUpdatables[i].OnLateUpdate(deltaTime);
        }

        [HideInCallstack]
        public static void AddUpdatable(IUpdateLoop updatable)
        {
            if (Instance._updatables.Contains(updatable))
                throw new System.InvalidOperationException($"This updatable already exists");

            Instance._updatables.Add(updatable);
        }

        [HideInCallstack]
        public static void AddFixedUpdatable(IFixedUpdateLoop fixedUpdatable)
        {
            if (Instance._fixedUpdatables.Contains(fixedUpdatable))
                throw new System.InvalidOperationException($"This fixedUpdatable already exists");

            Instance._fixedUpdatables.Add(fixedUpdatable);
        }

        [HideInCallstack]
        public static void AddLateUpdatable(ILateUpdateLoop lateUpdatable)
        {
            if (Instance._lateUpdatables.Contains(lateUpdatable))
                throw new System.InvalidOperationException($"This lateUpdatable already exists");

            Instance._lateUpdatables.Add(lateUpdatable);
        }

        public static void RemoveUpdatable(IUpdateLoop updatable) => Instance._updatables.Remove(updatable);

        public static void RemoveFixedUpdatable(IFixedUpdateLoop fixedUpdatable) => Instance._fixedUpdatables.Remove(fixedUpdatable);

        public static void RemoveLateUpdatable(ILateUpdateLoop lateUpdatable) => Instance._lateUpdatables.Remove(lateUpdatable);
    }
}