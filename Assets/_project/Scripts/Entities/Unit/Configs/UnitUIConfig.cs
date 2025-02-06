using _project.Scripts.Entities.Unit.UI.AbilityEffects;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Configs
{
    [CreateAssetMenu(
        fileName = "UnitUIConfig",
        menuName = "Config/Unit/UnitUIConfig",
        order = 0)]
    public class UnitUIConfig : ScriptableObject
    {
        [field: SerializeField] public AbilityEffectView AbilityEffectViewPrefab { get; private set; }
    }
}