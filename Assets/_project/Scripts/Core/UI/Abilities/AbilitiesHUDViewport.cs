using UnityEngine;

namespace _project.Scripts.Core.UI.Abilities
{
    public interface IAbilitiesHUDViewport
    {
        GameObject Content { get; }

        void Show();
        void Hide();
    }

    public class AbilitiesHUDViewport : MonoBehaviour, IAbilitiesHUDViewport
    {
        [field: SerializeField] public GameObject Content { get; private set; }

        public void Show()
        {
            Content.SetActive(true);
        }

        public void Hide()
        {
            Content.SetActive(false);
        }
    }
}