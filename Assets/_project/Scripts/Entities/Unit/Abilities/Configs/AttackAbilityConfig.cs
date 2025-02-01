using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Configs
{
    [CreateAssetMenu(
        fileName = "AttackAbilityConfig",
        menuName = "Config/Abilities/AttackAbilityConfig",
        order = 0)]
    public class AttackAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}