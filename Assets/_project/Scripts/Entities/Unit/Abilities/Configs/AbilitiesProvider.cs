using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Configs
{
    [CreateAssetMenu(
        fileName = "AbilitiesProvider",
        menuName = "Config/Abilities/AbilitiesProvider",
        order = 0)]
    public class AbilitiesProvider : ScriptableObject
    {
        [field: SerializeField] private AbilityConfig[] AbilityConfigs { get; set; }
    }
}