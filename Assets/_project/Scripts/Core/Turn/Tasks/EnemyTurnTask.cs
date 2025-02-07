using System;
using System.Threading;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using _project.Scripts.Entities.Unit.Enemy;
using Cysharp.Threading.Tasks;

namespace _project.Scripts.Core.Turn.Tasks
{
    public class EnemyTurnTask : Task, IDisposable
    {
        private const float Delay = 1.5f;

        private readonly IEnemyUnitAI _ai;
        private readonly IAbilityManager _abilityManager;
        private readonly IAbilityEffectsManager _abilityEffectsManager;

        private readonly CancellationTokenSource _cts;

        public EnemyTurnTask(
            IEnemyUnitAI ai,
            IAbilityManager abilityManager,
            IAbilityEffectsManager abilityEffectsManager)
        {
            _ai = ai;
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

            _abilityManager.TickCooldown();
            _abilityEffectsManager.Tick();
            _ai.UseRandomAbility(Finish);
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}