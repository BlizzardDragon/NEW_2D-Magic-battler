using System;
using UnityEngine;

namespace _project.Scripts.Entities.Health
{
    public interface IHealth
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
        bool IsDead { get; }

        event Action<int> DamageReceived;
        event Action<int> HealReceived;
        event Action HealthChanged;
        event Action Died;

        void TakeDamage(int damage);
        void Heal(int heal);
    }

    public class EntityHealth : IHealth
    {
        public EntityHealth(int maxHealth)
        {
            CurrentHealth = maxHealth;
            MaxHealth = maxHealth;
        }

        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }
        public bool IsDead { get; private set; }

        public event Action<int> DamageReceived;
        public event Action<int> HealReceived;
        public event Action HealthChanged;
        public event Action Died;

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Debug.LogError($"Negative damage received ({damage})");
                return;
            }

            if (IsDead) return;

            CurrentHealth -= damage;
            DamageReceived?.Invoke(damage);
            HealthChanged?.Invoke();

            if (CurrentHealth > 0) return;

            CurrentHealth = 0;
            IsDead = true;
            Died?.Invoke();
        }

        public void Heal(int heal)
        {
            if (heal < 0)
            {
                Debug.LogError($"Negative heal value received ({heal})");
                return;
            }

            if (IsDead) return;

            CurrentHealth += heal;
            HealReceived?.Invoke(heal);
            HealthChanged?.Invoke();

            if (CurrentHealth <= MaxHealth) return;

            CurrentHealth = MaxHealth;
        }
    }
}