using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Configs
{
    [CreateAssetMenu(
        fileName = "RegenerationAbilityConfig",
        menuName = "Config/Abilities/RegenerationAbilityConfig",
        order = 0)]
    public class RegenerationAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public int RegenerationPower { get; private set; }
        [field: SerializeField] public int Duration { get; private set; }
    }
}