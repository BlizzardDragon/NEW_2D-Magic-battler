using System;
using _project.Scripts.Entities.Unit.Abilities.Configs;

namespace _project.Scripts.Entities.Unit.Abilities
{
    public abstract class Ability
    {
        protected Ability(AbilityConfig config)
        {
            Config = config;
        }

        public string Name => Config.Name;
        public bool IsAvailable => Cooldown <= 0;
        public bool CooldownIsStopped { get; protected set; }
        public int Cooldown { get; private set; }
        public AbilityConfig Config { get; }

        public event Action CooldownUpdated;
        public event Action<Ability> Used;

        public void TickCooldown()
        {
            if (!CooldownIsStopped)
            {
                Cooldown--;
            }

            if (Cooldown < 0)
            {
                Cooldown = 0;
            }

            OnTickCooldown();
            CooldownUpdated?.Invoke();
        }

        protected virtual void OnTickCooldown()
        {
        }

        public void Use()
        {
            if (!IsAvailable) return;

            Cooldown = Config.Cooldown;
            CooldownUpdated?.Invoke();

            OnUse();
            Used?.Invoke(this);
        }

        protected abstract void OnUse();
    }
}