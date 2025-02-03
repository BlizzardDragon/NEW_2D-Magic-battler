using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Configs;
using _project.Scripts.Entities.Unit.Abilities.Effects;

namespace _project.Scripts.Entities.Unit.Abilities
{
    public class RegenerationAbility : Ability
    {
        private readonly IEntity _entity;
        private readonly IAbilityEffectsManager _effectsManager;
        private readonly RegenerationAbilityConfig _config;

        public RegenerationAbility(
            IEntity entity, IAbilityEffectsManager effectsManager, RegenerationAbilityConfig config) : base(config)
        {
            _entity = entity;
            _effectsManager = effectsManager;
            _config = config;
        }

        protected override void OnUse()
        {
            var effect = new RegenerationEffect(
                _entity.GetModule<IHealth>(),
                _config.EffectConfig.Duration,
                _config.EffectConfig);

            _effectsManager.AddEffect(effect);
        }
    }
}