using _project.Scripts.Entities.Unit.Abilities.Configs;
using _project.Scripts.Entities.Unit.UI;
using Entity.Core;
using UnityEngine;

namespace _project.Scripts.Entities.Unit
{
    public class UnitMono : MonoBehaviour
    {
        [field: SerializeField] public MonoEntity Target { get; private set; }
        [field: SerializeField] public HealthBarView HealthBarView { get; private set; }
        [field: SerializeField] public AbilitiesProvider AbilitiesProvider { get; private set; }
    }
}