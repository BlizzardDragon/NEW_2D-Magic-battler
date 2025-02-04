using _project.Scripts.Core.UI.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Configs;
using Entity.Core;
using UnityEngine;

namespace _project.Scripts.Entities.Unit
{
    public class UnitMono : MonoBehaviour
    {
        [field: SerializeField] public MonoEntity Target { get; private set; }
        [field: SerializeField] public AbilityButtonView AbilityButtonViewPrefab { get; private set; }
        [field: SerializeField] public AbilitiesProvider AbilitiesProvider { get; private set; }
    }
}