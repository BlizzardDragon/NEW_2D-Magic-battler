using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Configs;
using _project.Scripts.Entities.Unit.Abilities.Effects;

namespace _project.Scripts.Entities.Unit.Abilities
{
    [AbilityType(AbilityType.Barrier)]
    public class BarrierAbility : Ability
    {
        private readonly IHealth _health;
        private readonly IAbilityEffectsManager _effectsManager;
        private readonly BarrierAbilityConfig _config;

        public BarrierAbility(
            IHealth health, IAbilityEffectsManager effectsManager, BarrierAbilityConfig config) : base(config)
        {
            _health = health;
            _effectsManager = effectsManager;
            _config = config;
        }

        protected override void OnUse()
        {
            var effect = new BarrierEffect(_health, _config.EffectConfig.Duration, _config.EffectConfig);

            _effectsManager.AddEffect(effect);
        }
    }
}