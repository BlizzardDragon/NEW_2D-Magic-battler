using _project.Scripts.Entities.Unit.Abilities.Effects.Configs;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Configs
{
    [CreateAssetMenu(
        fileName = "RegenerationAbilityConfig",
        menuName = "Config/Abilities/RegenerationAbilityConfig",
        order = 0)]
    public class RegenerationAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public RegenerationEffectConfig EffectConfig { get; private set; }
    }
}