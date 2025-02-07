using System;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.Game.UI.RestartButton
{
    public class RestartButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event Action ButtonClicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            ButtonClicked?.Invoke();
        }
    }
}