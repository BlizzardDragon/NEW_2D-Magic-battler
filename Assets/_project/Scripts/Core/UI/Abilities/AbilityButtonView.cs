using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.Core.UI.Abilities
{
    public class AbilityButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _cooldownText;
        [SerializeField] private Image _icon;

        public Button Button => _button;

        public void EnableButton(bool enable)
        {
            _button.interactable = enable;
        }

        public void RenderCooldown(string text)
        {
            _cooldownText.text = text;
        }

        public void RenderIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}