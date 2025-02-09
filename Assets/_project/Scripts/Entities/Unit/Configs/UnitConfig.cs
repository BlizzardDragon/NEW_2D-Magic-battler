using UnityEngine;

namespace _project.Scripts.Entities.Unit.Configs
{
    [CreateAssetMenu(
        fileName = "UnitConfig",
        menuName = "Config/Unit/UnitConfig",
        order = 0)]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public int StartHealth { get; private set; } = 100;
    }
}