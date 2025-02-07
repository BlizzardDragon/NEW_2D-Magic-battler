using System;
using System.Threading;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _project.Scripts.Core.Turn.Tasks
{
    public class PlayerTurnTask : Task, IDisposable
    {
        private const float Delay = 1.5f;
        
        private readonly IAbilityManager _abilityManager;
        private readonly IAbilityEffectsManager _abilityEffectsManager;

        private readonly CancellationTokenSource _cts;

        public PlayerTurnTask(IAbilityManager abilityManager, IAbilityEffectsManager abilityEffectsManager)
        {
            _abilityManager = abilityManager;
            _abilityEffectsManager = abilityEffectsManager;

            _cts = new CancellationTokenSource();
        }

        protected override void OnRun()
        {
            OnRunAsync().Forget();
        }

        private async UniTask OnRunAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Delay), cancellationToken: _cts.Token);
            
            _abilityManager.EnableAbilities(true);

            foreach (var ability in _abilityManager.Abilities)
            {
                ability.Used += OnUsed;
            }

            _abilityManager.TickCooldown();
            _abilityEffectsManager.Tick();
        }

        protected override void OnFinish()
        {
            _abilityManager.EnableAbilities(false);

            foreach (var ability in _abilityManager.Abilities)
            {
                ability.Used -= OnUsed;
            }
        }

        private void OnUsed(Ability ability)
        {
            Debug.Log($"Player used: {ability.Name}");
            Finish();
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}