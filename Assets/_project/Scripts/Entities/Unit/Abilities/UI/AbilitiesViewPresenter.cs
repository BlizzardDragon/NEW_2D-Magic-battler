using _project.Scripts.Core.UI.Abilities;

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
        }

        public void OnDisable()
        {
            _view.Button.onClick.RemoveListener(OnButtonClicked);
            _model.CooldownUpdated -= UpdateViewState;
        }

        private void OnButtonClicked()
        {
            _model.Use();
            UpdateViewState();
        }

        private void UpdateViewState()
        {
            var cooldown = _model.Cooldown;

            if (cooldown <= 0)
            {
                _view.EnableButton(true);
                _view.RenderCooldown("");
            }
            else
            {
                _view.EnableButton(false);
                _view.RenderCooldown(cooldown.ToString());
            }
        }
    }
}