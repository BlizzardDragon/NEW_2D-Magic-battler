using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using _project.Scripts.Game.Commands;
using VampireSquid.Common.Commands;

namespace _project.Scripts.Entities.Unit
{
    public class UnitFinishGameListener : ICommandListener<FinishGameCommand>
    {
        private readonly IAbilityManager _abilityManager;
        private readonly IAbilityEffectsManager _abilityEffectsManager;

        public UnitFinishGameListener(IAbilityManager abilityManager, IAbilityEffectsManager abilityEffectsManager)
        {
            _abilityManager = abilityManager;
            _abilityEffectsManager = abilityEffectsManager;
        }

        public void ReactCommand(FinishGameCommand command)
        {
            _abilityManager.EnableAbilities(false);
            _abilityEffectsManager.RemoveAllEffects();
        }
    }
}