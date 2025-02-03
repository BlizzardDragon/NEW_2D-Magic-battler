using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Effects.Configs;

namespace _project.Scripts.Entities.Unit.Abilities.Effects
{
    public class BurnEffect : AbilityEffect
    {
        private readonly IHealth _health;
        private readonly BurnEffectConfig _config;

        public BurnEffect(IHealth health, int duration, BurnEffectConfig config) : base(duration, config)
        {
            _health = health;
            _config = config;
        }

        protected override void OnTick()
        {
            _health.TakeDamage(_config.BurnDamage);
        }
    }
}