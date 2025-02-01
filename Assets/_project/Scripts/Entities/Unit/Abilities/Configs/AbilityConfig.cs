using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Configs
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [field: SerializeField] public AbilityType Type { get; private set; }
        [field: SerializeField] public int Cooldown { get; private set; }
    }
}