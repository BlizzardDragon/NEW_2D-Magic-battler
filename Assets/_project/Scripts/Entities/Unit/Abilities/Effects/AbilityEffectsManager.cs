using System;
using System.Collections.Generic;
using System.Linq;

namespace _project.Scripts.Entities.Unit.Abilities.Effects
{
    public interface IAbilityEffectsManager
    {
        event Action<AbilityEffect> EffectAdded;
        event Action<AbilityEffect> EffectEnded;

        void Tick();
        void AddEffect(AbilityEffect effect);
        void RemoveEffects<T>() where T : AbilityEffect;
        bool TryGetEffect<T>(out T effect) where T : AbilityEffect;
    }

    public class AbilityEffectsManager : IAbilityEffectsManager
    {
        private readonly List<AbilityEffect> _effects = new();

        public event Action<AbilityEffect> EffectAdded;
        public event Action<AbilityEffect> EffectEnded;

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

        public void RemoveEffects<T>() where T : AbilityEffect
        {
            foreach (var effect in _effects.OfType<T>())
            {
                effect.StopEffect();
            }
        }

        public bool TryGetEffect<T>(out T effect) where T : AbilityEffect
        {
            foreach (var currentEffect in _effects.OfType<T>())
            {
                effect = currentEffect;
                return true;
            }

            effect = default;
            return false;
        }

        private void OnEffectEnded(AbilityEffect effect)
        {
            effect.EffectEnded -= OnEffectEnded;
            _effects.Remove(effect);
            EffectEnded?.Invoke(effect);
        }
    }
}