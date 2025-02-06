using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.Entities.Unit.UI
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private TMP_Text _healthPoints;

        public void RenderFillAmount(float fillAmount)
        {
            _healthBar.fillAmount = fillAmount;
        }

        public void RenderHealth(string text)
        {
            _healthPoints.text = text;
        }
    }
}