using _project.Scripts.Core.Turn;
using _project.Scripts.Core.Turn.Tasks;
using _project.Scripts.Core.UI.Abilities;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Player.Compositions
{
    public class PlayerTurnComposition : EntityModuleCompositionBase
    {
        private ITurnPipeline _turnPipeline;
        private PlayerTurnTask _task;

        public override void Create(IEntity entity)
        {
            var abilityManager = entity.GetModule<IAbilityManager>();
            var abilityEffectsManager = entity.GetModule<IAbilityEffectsManager>();
            var abilitiesHudViewport = Get<IAbilitiesHUDViewport>();
            _turnPipeline = Get<ITurnPipeline>();

            _task = new PlayerTurnTask(abilityManager, abilityEffectsManager, abilitiesHudViewport);
        }

        public override void Initialize()
        {
            _turnPipeline.AddTask(_task);
        }
    }
}