using _project.Scripts.Core.UI.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Configs;
using _project.Scripts.Entities.Unit.Abilities.Network;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.UI
{
    public class AbilityViewPresenter
    {
        private readonly AbilityButtonView _view;
        private readonly INetworkAbilitiesAdapter _networkAbilitiesAdapter;
        private readonly AbilityConfig _config;

        public AbilityViewPresenter(
            AbilityButtonView view,
            INetworkAbilitiesAdapter networkAbilitiesAdapter,
            AbilityConfig config)
        {
            _view = view;
            _networkAbilitiesAdapter = networkAbilitiesAdapter;
            _config = config;
        }

        public void OnEnable()
        {
            _view.RenderIcon(_config.Sprite);

            _view.Button.onClick.AddListener(OnButtonClicked);
            _networkAbilitiesAdapter.ServerUpdatedAbilityCooldown += UpdateCooldown;
            _networkAbilitiesAdapter.ServerUpdatedAbilityEnable += UpdateEnable;

            _networkAbilitiesAdapter.RequestUpdateAbilityState_Client(_config.Type);
        }

        public void OnDisable()
        {
            _view.Button.onClick.RemoveListener(OnButtonClicked);
            _networkAbilitiesAdapter.ServerUpdatedAbilityCooldown -= UpdateCooldown;
            _networkAbilitiesAdapter.ServerUpdatedAbilityEnable -= UpdateEnable;

            Object.Destroy(_view.gameObject);
        }

        private void OnButtonClicked()
        {
            _networkAbilitiesAdapter.UseAbilityRequest_Client(_config.Type);
        }

        private void UpdateCooldown(AbilityType type, int? cooldown)
        {
            if (type != _config.Type) return;

            var text = cooldown == null ? "" : cooldown.ToString();
            _view.RenderCooldown(text);
        }

        private void UpdateEnable(AbilityType type, bool enable)
        {
            if (type != _config.Type) return;

            _view.EnableButton(enable);
        }
    }
}