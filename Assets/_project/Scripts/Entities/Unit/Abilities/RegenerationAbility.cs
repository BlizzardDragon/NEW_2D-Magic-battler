using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Configs;
using _project.Scripts.Entities.Unit.Abilities.Effects;

namespace _project.Scripts.Entities.Unit.Abilities
{
    [AbilityType(AbilityType.Regeneration)]
    public class RegenerationAbility : Ability
    {
        private readonly IHealth _health;
        private readonly IAbilityEffectsManager _effectsManager;
        private readonly RegenerationAbilityConfig _config;

        public RegenerationAbility(
            IHealth health, IAbilityEffectsManager effectsManager, RegenerationAbilityConfig config) : base(config)
        {
            _health = health;
            _effectsManager = effectsManager;
            _config = config;
        }

        protected override void OnUse()
        {
            var effect = new RegenerationEffect(_health, _config.EffectConfig.Duration, _config.EffectConfig);

            _effectsManager.AddEffect(effect);
        }
    }
}