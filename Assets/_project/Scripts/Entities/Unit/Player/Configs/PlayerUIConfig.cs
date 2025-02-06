using _project.Scripts.Core.UI.Abilities;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Player.Configs
{
    [CreateAssetMenu(
        fileName = "PlayerUIConfig",
        menuName = "Config/Unit/PlayerUIConfig",
        order = 0)]
    public class PlayerUIConfig : ScriptableObject
    {
        [field: SerializeField] public AbilityButtonView AbilityButtonViewPrefab { get; private set; }
    }
}