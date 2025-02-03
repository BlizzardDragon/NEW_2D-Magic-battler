using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Configs;

namespace _project.Scripts.Entities.Unit.Abilities
{
    public class RegenerationAbility : Ability
    {
        private readonly IHealth _health;
        private readonly RegenerationAbilityConfig _config;

        private int _duration;

        public RegenerationAbility(IHealth health, RegenerationAbilityConfig config) : base(config)
        {
            _health = health;
            _config = config;
        }

        protected override void OnUse()
        {
            _duration = _config.Duration;
        }

        protected override void OnTickCooldown()
        {
            base.OnTickCooldown();

            if (_duration <= 0) return;

            _health.Heal(_config.RegenerationPower);
            _duration--;
        }
    }
}