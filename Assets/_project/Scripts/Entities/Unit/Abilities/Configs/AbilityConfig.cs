using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.Configs
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;

        [field: SerializeField] public AbilityType Type { get; private set; }
        [field: SerializeField] public int Cooldown { get; private set; }

        public Sprite Sprite => _sprite;
        public string Name => Type.ToString();
    }
}