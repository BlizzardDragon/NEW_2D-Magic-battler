using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Effects.Configs
{
    [CreateAssetMenu(
        fileName = "RegenerationEffectConfig",
        menuName = "Config/Abilities/Effects/RegenerationEffectConfig",
        order = 0)]
    public class RegenerationEffectConfig : AbilityEffectConfig
    {
        [field: SerializeField] public int RegenerationPower { get; private set; }
    }
}