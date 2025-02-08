using _project.Scripts.Core.Turn.Tasks;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Player.Compositions
{
    public class ServerPlayerTurnComposition : EntityModuleCompositionBase
    {
        private PlayerTurnTask _task;

        public override void Create(IEntity entity)
        {
            var abilityManager = entity.GetModule<IAbilityManager>();
            var abilityEffectsManager = entity.GetModule<IAbilityEffectsManager>();

            _task = new PlayerTurnTask(abilityManager, abilityEffectsManager);

            entity.AddModule<Task>(_task);
        }

        protected override void OnBeforeDestroy()
        {
            _task.Dispose();
        }
    }
}