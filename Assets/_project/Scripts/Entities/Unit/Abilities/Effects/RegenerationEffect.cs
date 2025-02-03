using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Effects.Configs;

namespace _project.Scripts.Entities.Unit.Abilities.Effects
{
    public class RegenerationEffect : AbilityEffect
    {
        private readonly IHealth _health;
        private readonly RegenerationEffectConfig _config;

        public RegenerationEffect(
            IHealth health, int duration, RegenerationEffectConfig config) : base(duration, config)
        {
            _health = health;
            _config = config;
        }

        protected override void OnTick()
        {
            _health.Heal(_config.RegenerationPower);
        }
    }
}