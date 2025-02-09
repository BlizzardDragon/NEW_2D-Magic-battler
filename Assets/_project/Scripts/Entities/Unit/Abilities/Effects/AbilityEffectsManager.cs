using System;
using System.Collections.Generic;
using System.Linq;

namespace _project.Scripts.Entities.Unit.Abilities.Effects
{
    public interface IAbilityEffectsManager
    {
        IReadOnlyList<AbilityEffect> Effects { get; }

        event Action<AbilityEffect> EffectAdded;
        event Action<AbilityEffect> EffectEnded;

        void Tick();
        void AddEffect(AbilityEffect effect);
        void RemoveEffects<T>() where T : AbilityEffect;
        bool TryGetEffect<T>(out T effect) where T : AbilityEffect;
        void RemoveAllEffects();
    }

    public class AbilityEffectsManager : IAbilityEffectsManager
    {
        private readonly List<AbilityEffect> _effects = new();

        public IReadOnlyList<AbilityEffect> Effects => _effects;

        public event Action<AbilityEffect> EffectAdded;
        public event Action<AbilityEffect> EffectEnded;

        public void Tick()
        {
            for (var i = _effects.Count - 1; i >= 0; i--)
            {
                _effects[i].Tick();
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
            var effect = _effects.OfType<T>().FirstOrDefault();
            effect?.StopEffect();
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

        public void RemoveAllEffects()
        {
            for (var i = _effects.Count - 1; i >= 0; i--)
            {
                _effects[i].StopEffect();
            }
        }

        private void OnEffectEnded(AbilityEffect effect)
        {
            effect.EffectEnded -= OnEffectEnded;
            _effects.Remove(effect);
            EffectEnded?.Invoke(effect);
        }
    }
}