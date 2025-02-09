using System;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Effects.Configs
{
    [CreateAssetMenu(
        fileName = "AbilityEffectsProvider",
        menuName = "Config/Abilities/Effects/AbilityEffectsProvider",
        order = 0)]
    public class AbilityEffectsProvider : ScriptableObject
    {
        [field: SerializeField] public AbilityEffectConfig[] EffectConfigs { get; private set; }

        public AbilityEffectConfig GetConfig(AbilityEffectType type)
        {
            foreach (var effect in EffectConfigs)
            {
                if (effect.Type == type)
                {
                    return effect;
                }
            }

            throw new AggregateException($"Type ({type}) not found!");
        }
    }
}