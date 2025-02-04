using _project.Scripts.Entities.Unit.Abilities.Configs;
using _project.Scripts.Entities.Unit.Abilities.Effects;

namespace _project.Scripts.Entities.Unit.Abilities
{
    [AbilityType(AbilityType.Purification)]
    public class PurificationAbility : Ability
    {
        private readonly IAbilityEffectsManager _effectsManager;

        public PurificationAbility(AbilityConfig config, IAbilityEffectsManager effectsManager) : base(config)
        {
            _effectsManager = effectsManager;
        }

        protected override void OnUse()
        {
            _effectsManager.RemoveEffects<BurnEffect>();
        }
    }
}