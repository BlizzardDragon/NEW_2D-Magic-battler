using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Effects;

namespace _project.Scripts.Entities.Unit
{
    public class UnitDamageReceiver : DamageReceiverBase
    {
        private readonly IHealth _health;
        private readonly IAbilityEffectsManager _effectsManager;

        public UnitDamageReceiver(IHealth health, IAbilityEffectsManager effectsManager) : base(health)
        {
            _health = health;
            _effectsManager = effectsManager;
        }

        protected override void OnTakeDamage(int damage)
        {
            if (_effectsManager.TryGetEffect(out BarrierEffect barrierEffect))
            {
                barrierEffect.TakeDamage(damage);
            }
            else
            {
                _health.TakeDamage(damage);
            }
        }
    }
}