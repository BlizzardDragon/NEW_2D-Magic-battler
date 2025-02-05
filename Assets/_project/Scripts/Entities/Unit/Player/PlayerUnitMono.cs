using _project.Scripts.Core.UI.Abilities;
using UnityEngine;

namespace _project.Scripts.Entities.Unit
{
    public class PlayerUnitMono : MonoBehaviour
    {
        [field: SerializeField] public AbilityButtonView AbilityButtonViewPrefab { get; private set; }
    }
}