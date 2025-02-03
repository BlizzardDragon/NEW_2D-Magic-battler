using System;
using System.Collections.Generic;
using System.Linq;

namespace _project.Scripts.Entities.Unit.Abilities.Effects
{
    public interface IAbilityEffectsManager
    {
        event Action<AbilityEffect> EffectAdded;

        void Tick();
        void AddEffect(AbilityEffect effect);
        void RemoveEffect<T>() where T : AbilityEffect;
    }

    public class AbilityEffectsManager : IAbilityEffectsManager
    {
        private readonly List<AbilityEffect> _effects = new();

        public event Action<AbilityEffect> EffectAdded;

        public void Tick()
        {
            foreach (var effect in _effects)
            {
                effect.Tick();
            }
        }

        public void AddEffect(AbilityEffect effect)
        {
            _effects.Add(effect);
            effect.EffectEnded += OnEffectEnded;
            EffectAdded?.Invoke(effect);
        }

        public void RemoveEffect<T>() where T : AbilityEffect
        {
            foreach (var effect in _effects.OfType<T>())
            {
                effect.StopEffect();
            }
        }

        private void OnEffectEnded(AbilityEffect effect)
        {
            effect.EffectEnded -= OnEffectEnded;
            _effects.Remove(effect);
        }
    }
}