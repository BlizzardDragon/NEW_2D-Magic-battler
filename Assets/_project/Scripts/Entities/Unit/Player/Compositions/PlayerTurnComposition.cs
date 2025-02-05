using _project.Scripts.Core.Turn;
using _project.Scripts.Core.Turn.Tasks;
using _project.Scripts.Core.UI.Abilities;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class PlayerTurnComposition : EntityModuleCompositionBase
    {
        public override void Create(IEntity entity)
        {
            var abilityManager = entity.GetModule<IAbilityManager>();
            var abilityEffectsManager = entity.GetModule<IAbilityEffectsManager>();
            var abilitiesHudViewport = Get<IAbilitiesHUDViewport>();
            var turnPipeline = Get<ITurnPipeline>();

            var playerTurnTask = new PlayerTurnTask(abilityManager, abilityEffectsManager, abilitiesHudViewport);

            turnPipeline.AddTask(playerTurnTask);
        }
    }
}