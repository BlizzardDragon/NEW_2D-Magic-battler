using _project.Scripts.Core.Turn.Tasks;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Enemy.Compositions
{
    public class EnemyTurnComposition : EntityModuleCompositionBase
    {
        private EnemyUnitAI _ai;
        private EnemyTurnTask _task;

        public override void Create(IEntity entity)
        {
            var abilityManager = entity.GetModule<IAbilityManager>();
            var abilityEffectsManager = entity.GetModule<IAbilityEffectsManager>();

            _ai = new EnemyUnitAI(abilityManager);
            _task = new EnemyTurnTask(_ai, abilityManager, abilityEffectsManager);

            entity.AddModule<Task>(_task);
        }

        protected override void OnBeforeDestroy()
        {
            _ai.Dispose();
            _task.Dispose();
        }
    }
}