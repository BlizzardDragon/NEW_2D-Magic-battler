using UnityEngine;

namespace _project.Scripts.Core.UI.Abilities
{
    public interface IAbilitiesHUDViewport
    {
        GameObject Content { get; }
    }

    public class AbilitiesHUDViewport : MonoBehaviour, IAbilitiesHUDViewport
    {
        [field: SerializeField] public GameObject Content { get; private set; }
    }
}