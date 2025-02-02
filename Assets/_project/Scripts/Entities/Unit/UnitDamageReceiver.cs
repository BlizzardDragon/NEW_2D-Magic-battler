using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities;

namespace _project.Scripts.Entities.Unit
{
    public class UnitDamageReceiver : DamageReceiverBase
    {
        private readonly BarrierAbility _barrierAbility;

        public UnitDamageReceiver(IHealth health, BarrierAbility barrierAbility) : base(health)
        {
            _barrierAbility = barrierAbility;
        }

        protected override void OnTakeDamage(int damage)
        {
            _barrierAbility.TakeDamage(damage);
        }
    }
}