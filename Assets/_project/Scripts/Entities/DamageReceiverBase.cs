using _project.Scripts.Entities.Health;

namespace _project.Scripts.Entities
{
    public interface IDamageReceiver
    {
        void TakeDamage(int damage);
    }

    public abstract class DamageReceiverBase : IDamageReceiver
    {
        protected readonly IHealth Health;

        protected DamageReceiverBase(IHealth health)
        {
            Health = health;
        }

        public void TakeDamage(int damage)
        {
            if (!Health.IsDead)
            {
                OnTakeDamage(damage);
            }
        }

        protected abstract void OnTakeDamage(int damage);
    }
}