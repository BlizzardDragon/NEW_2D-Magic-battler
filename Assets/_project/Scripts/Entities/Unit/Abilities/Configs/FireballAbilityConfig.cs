using _project.Scripts.Entities.Unit.Abilities.Effects.Configs;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Configs
{
    [CreateAssetMenu(
        fileName = "FireballAbilityConfig",
        menuName = "Config/Abilities/FireballAbilityConfig",
        order = 0)]
    public class FireballAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public BurnEffectConfig EffectConfig { get; private set; }
    }
}