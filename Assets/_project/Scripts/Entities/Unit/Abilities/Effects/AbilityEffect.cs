using System;
using _project.Scripts.Entities.Unit.Abilities.Effects.Configs;

namespace _project.Scripts.Entities.Unit.Abilities.Effects
{
    public abstract class AbilityEffect
    {
        protected AbilityEffect(int duration, AbilityEffectConfig config)
        {
            Duration = config.Duration;
            Config = config;
        }

        public int Duration { get; private set; }
        public AbilityEffectConfig Config { get; }

        public event Action<AbilityEffect> EffectEnded;

        public void Tick()
        {
            if (Duration <= 0) return;

            Duration--;
            OnTick();

            if (Duration <= 0)
            {
                EffectEnded?.Invoke(this);
            }
        }

        public void StopEffect()
        {
            Duration = 0;
            EffectEnded?.Invoke(this);
        }

        protected abstract void OnTick();
    }
}