using _project.Scripts.Core.UI.Abilities;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.UI
{
    public class AbilitiesViewPresenter
    {
        private readonly Ability _model;
        private readonly AbilityButtonView _view;

        public AbilitiesViewPresenter(Ability model, AbilityButtonView view)
        {
            _model = model;
            _view = view;
        }

        public void OnEnable()
        {
            _view.RenderIcon(_model.Config.Sprite);
            UpdateViewState();

            _view.Button.onClick.AddListener(OnButtonClicked);
            _model.CooldownUpdated += UpdateViewState;
            _model.Enabled += OnAbilityEnabled;
        }

        public void OnDisable()
        {
            _view.Button.onClick.RemoveListener(OnButtonClicked);
            _model.CooldownUpdated -= UpdateViewState;
            _model.Enabled -= OnAbilityEnabled;
        }

        private void OnButtonClicked()
        {
            _model.Use();
            UpdateViewState();
        }

        private void UpdateViewState()
        {
            if (!_model.IsEnable)
            {
                _view.EnableButton(false);
                RenderEmpty();
                return;
            }

            if (_model.CooldownIsOver)
            {
                _view.EnableButton(true);
                RenderEmpty();
            }
            else
            {
                if (_model.CooldownIsStopped)
                {
                    _view.EnableButton(false);
                    RenderEmpty();
                }
                else
                {
                    _view.EnableButton(false);
                    _view.RenderCooldown(_model.Cooldown.ToString());
                }
            }
        }

        private void RenderEmpty()
        {
            _view.RenderCooldown("");
        }

        private void OnAbilityEnabled(bool enable)
        {
            _view.EnableButton(enable);
        }
    }
}