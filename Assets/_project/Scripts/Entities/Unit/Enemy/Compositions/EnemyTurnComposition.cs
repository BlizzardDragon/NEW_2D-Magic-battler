using _project.Scripts.Core.Turn;
using _project.Scripts.Core.Turn.Tasks;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Enemy.Compositions
{
    public class EnemyTurnComposition : EntityModuleCompositionBase
    {
        private ITurnPipeline _turnPipeline;
        private EnemyTurnTask _task;
        private EnemyUnitAI _ai;

        public override void Create(IEntity entity)
        {
            var abilityManager = entity.GetModule<IAbilityManager>();
            var abilityEffectsManager = entity.GetModule<IAbilityEffectsManager>();
            _turnPipeline = Get<ITurnPipeline>();

            _ai = new EnemyUnitAI(abilityManager);
            _task = new EnemyTurnTask(_ai, abilityManager, abilityEffectsManager);
        }

        public override void Initialize()
        {
            _turnPipeline.AddTask(_task);
        }

        protected override void OnBeforeDestroy()
        {
            _ai.Dispose();
        }
    }
}