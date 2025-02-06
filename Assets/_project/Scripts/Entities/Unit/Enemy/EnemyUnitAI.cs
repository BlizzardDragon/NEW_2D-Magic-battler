using System;
using System.Collections.Generic;
using System.Threading;
using _project.Scripts.Entities.Unit.Abilities;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.Scripts.Entities.Unit.Enemy
{
    public interface IEnemyUnitAI
    {
        UniTask UseRandomAbility(Action callback);
    }

    public class EnemyUnitAI : IEnemyUnitAI, IDisposable
    {
        private const float Delay = 1.5f;

        private readonly IAbilityManager _abilityManager;

        private readonly List<Ability> _availableAbilities = new();
        private readonly CancellationTokenSource _cts;

        public EnemyUnitAI(IAbilityManager abilityManager)
        {
            _abilityManager = abilityManager;

            _cts = new CancellationTokenSource();
        }

        public async UniTask UseRandomAbility(Action callback)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Delay), cancellationToken: _cts.Token);

            _availableAbilities.Clear();

            foreach (var ability in _abilityManager.Abilities)
            {
                if (ability.CooldownIsOver)
                {
                    _availableAbilities.Add(ability);
                }
            }

            var randomIndex = Random.Range(0, _availableAbilities.Count);
            _availableAbilities[randomIndex].Use();

            Debug.Log($"Enemy used: {_availableAbilities[randomIndex].Name}");

            callback?.Invoke();
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}