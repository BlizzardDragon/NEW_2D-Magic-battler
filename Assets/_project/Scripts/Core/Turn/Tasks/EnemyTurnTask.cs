using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using _project.Scripts.Entities.Unit.Enemy;

namespace _project.Scripts.Core.Turn.Tasks
{
    public class EnemyTurnTask : Task
    {
        private readonly IEnemyUnitAI _ai;
        private readonly IAbilityManager _abilityManager;
        private readonly IAbilityEffectsManager _abilityEffectsManager;

        public EnemyTurnTask(
            IEnemyUnitAI ai,
            IAbilityManager abilityManager,
            IAbilityEffectsManager abilityEffectsManager)
        {
            _ai = ai;
            _abilityManager = abilityManager;
            _abilityEffectsManager = abilityEffectsManager;
        }

        protected override void OnRun()
        {
            _abilityManager.TickCooldown();
            _abilityEffectsManager.Tick();
            _ai.UseRandomAbility(Finish);
        }
    }
}