using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using Entity.Core;
using VampireSquid.Common.Commands;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class ServerUnitFinishGameComposition : EntityModuleCompositionBase
    {
        public override void Create(IEntity entity)
        {
            var abilityManager = entity.GetModule<IAbilityManager>();
            var effectsManager = entity.GetModule<IAbilityEffectsManager>();
            var commandHandler = Get<IGlobalCommandHandler>();

            var finishGameListener = new UnitFinishGameListener(abilityManager, effectsManager);

            commandHandler.AddListener(finishGameListener);
        }
    }
}