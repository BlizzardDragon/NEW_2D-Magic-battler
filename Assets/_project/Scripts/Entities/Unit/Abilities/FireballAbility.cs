using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Configs;
using _project.Scripts.Entities.Unit.Abilities.Effects;

namespace _project.Scripts.Entities.Unit.Abilities
{
    [AbilityType(AbilityType.Fireball)]
    public class FireballAbility : Ability
    {
        private readonly IEntityTargetService _targetService;
        private readonly FireballAbilityConfig _config;

        public FireballAbility(IEntityTargetService targetService, FireballAbilityConfig config) : base(config)
        {
            _targetService = targetService;
            _config = config;
        }

        protected override void OnUse()
        {
            var target = _targetService.Target;

            if (target.TryGetModule(out IDamageReceiver damageReceiver))
            {
                damageReceiver.TakeDamage(_config.Damage);
            }

            if (target.TryGetModule(out IAbilityEffectsManager effectsManager))
            {
                if (target.TryGetModule(out IHealth health))
                {
                    CooldownIsStopped = true;

                    var effect = new BurnEffect(health, _config.EffectConfig.Duration, _config.EffectConfig);
                    effectsManager.AddEffect(effect);

                    effect.EffectEnded += OnEffectEnded;
                }
            }
        }

        private void OnEffectEnded(AbilityEffect abilityEffect)
        {
            CooldownIsStopped = false;
            abilityEffect.EffectEnded -= OnEffectEnded;
        }
    }
}