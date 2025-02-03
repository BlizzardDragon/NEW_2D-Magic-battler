using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Effects.Configs
{
    [CreateAssetMenu(
        fileName = "BarrierEffectConfig",
        menuName = "Config/Abilities/Effects/BarrierEffectConfig",
        order = 0)]
    public class BarrierEffectConfig : AbilityEffectConfig
    {
        [field: SerializeField] public int BlockDamage { get; private set; }
    }
}