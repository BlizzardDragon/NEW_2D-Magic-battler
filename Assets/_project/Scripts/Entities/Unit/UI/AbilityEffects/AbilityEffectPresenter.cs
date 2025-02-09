using _project.Scripts.Entities.Unit.Abilities.Effects;
using _project.Scripts.Entities.Unit.Abilities.Effects.Configs;
using _project.Scripts.Entities.Unit.Abilities.Effects.Network;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.UI.AbilityEffects
{
    public class AbilityEffectPresenter
    {
        private readonly INetworkAbilityEffectAdapter _adapter;
        private readonly AbilityEffectView _view;
        private readonly AbilityEffectConfig _config;

        public AbilityEffectPresenter(
            INetworkAbilityEffectAdapter adapter,
            AbilityEffectView view,
            AbilityEffectConfig config)
        {
            _adapter = adapter;
            _view = view;
            _config = config;

            _view.RenderIcon(_config.Sprite);
            _view.RenderDuration(_config.Duration.ToString());
        }

        public void OnEnable()
        {
            _adapter.ServerDurationUpdated += RenderDuration;
            _adapter.ServerEffectEnded += OnEffectEnded;
        }

        private void OnDisable()
        {
            _adapter.ServerDurationUpdated -= RenderDuration;
            _adapter.ServerEffectEnded -= OnEffectEnded;
        }

        private void RenderDuration(AbilityEffectType type, int duration)
        {
            if (type != _config.Type) return;

            _view.RenderDuration(duration.ToString());
        }

        private void OnEffectEnded(AbilityEffectType type)
        {
            if (type != _config.Type) return;

            OnDisable();
            Object.Destroy(_view.gameObject);
        }
    }
}