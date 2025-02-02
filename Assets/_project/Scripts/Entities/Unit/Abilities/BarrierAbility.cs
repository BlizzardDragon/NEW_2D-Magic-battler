using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Configs;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities
{
    public class BarrierAbility : Ability
    {
        private readonly IHealth _health;
        private readonly BarrierAbilityConfig _config;

        private int _duration;

        public BarrierAbility(IHealth health, BarrierAbilityConfig config) : base(config)
        {
            _health = health;
            _config = config;
        }

        public int BarrierValue { get; private set; }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Debug.LogError($"Negative value received ({damage})!");
                return;
            }

            BarrierValue -= damage;

            if (BarrierValue < 0)
            {
                _health.TakeDamage(Mathf.Abs(BarrierValue));
                BarrierValue = 0;
            }
        }

        protected override void OnUse()
        {
            BarrierValue = _config.BlockDamage;
            _duration = _config.Duration;
        }

        protected override void OnTickCooldown()
        {
            base.OnTickCooldown();

            _duration--;

            if (_duration <= 0)
            {
                BarrierValue = 0;
            }
        }
    }
}