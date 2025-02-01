using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Configs
{
    [CreateAssetMenu(
        fileName = "BarrierAbilityConfig",
        menuName = "Config/Abilities/BarrierAbilityConfig",
        order = 0)]
    public class BarrierAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public int BlockDamage { get; private set; }
        [field: SerializeField] public int Duration { get; private set; }
    }
}