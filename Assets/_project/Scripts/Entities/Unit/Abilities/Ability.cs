using System;
using _project.Scripts.Entities.Unit.Abilities.Configs;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities
{
    public abstract class Ability
    {
        private bool _isEnable;
        
        protected Ability(AbilityConfig config)
        {
            Config = config;
        }

        public string Name => Config.Name;
        public bool CooldownIsOver => Cooldown <= 0;
        public bool CooldownIsStopped { get; protected set; }
        public int Cooldown { get; private set; }
        public AbilityConfig Config { get; }

        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (_isEnable != value)
                {
                    _isEnable = value;
                    Enabled?.Invoke(_isEnable);
                }
            }
        }

        public event Action CooldownUpdated;
        public event Action<Ability> Used;
        public event Action<bool> Enabled;

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
            if (!CooldownIsOver) return;

            Cooldown = Config.Cooldown;
            CooldownUpdated?.Invoke();

            OnUse();
            Used?.Invoke(this);
        }

        protected abstract void OnUse();
    }
}