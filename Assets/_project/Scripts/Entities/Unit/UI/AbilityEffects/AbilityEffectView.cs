using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.Entities.Unit.UI.AbilityEffects
{
    public class AbilityEffectView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _countdown;

        public void RenderIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void RenderDuration(string text)
        {
            _countdown.text = text;
        }
    }
}