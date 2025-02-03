using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Effects.Configs
{
    [CreateAssetMenu(
        fileName = "BurnEffectConfig",
        menuName = "Config/Abilities/Effects/BurnEffectConfig",
        order = 0)]
    public class BurnEffectConfig : AbilityEffectConfig
    {
        [field: SerializeField] public int BurnDamage { get; private set; }
    }
}