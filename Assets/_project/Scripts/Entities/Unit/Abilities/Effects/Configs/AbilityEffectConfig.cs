using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Effects.Configs
{
    public class AbilityEffectConfig : ScriptableObject
    {
        [field: SerializeField] public AbilityEffectType Type { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int Duration { get; private set; }
    }
}