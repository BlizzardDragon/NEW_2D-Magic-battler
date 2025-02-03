using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Effects.Configs;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Effects
{
    public class BarrierEffect : AbilityEffect
    {
        private readonly IHealth _health;

        public BarrierEffect(IHealth health, int duration, BarrierEffectConfig config) : base(duration, config)
        {
            _health = health;
            BarrierValue = config.BlockDamage;
        }

        public int BarrierValue { get; private set; }

        protected override void OnTick()
        {
            if (Duration <= 0)
            {
                BarrierValue = 0;
            }
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Debug.LogError($"Negative value received ({damage})!");
                return;
            }

            BarrierValue -= damage;

            if (BarrierValue <= 0)
            {
                _health.TakeDamage(Mathf.Abs(BarrierValue));
                BarrierValue = 0;
                StopEffect();
            }
        }
    }
}