using _project.Scripts.Entities.Unit.Abilities.Effects;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.UI.AbilityEffects
{
    public class AbilityEffectPresenter
    {
        private readonly AbilityEffect _model;
        private readonly AbilityEffectView _view;

        public AbilityEffectPresenter(AbilityEffect model, AbilityEffectView view)
        {
            _model = model;
            _view = view;

            _view.RenderIcon(_model.Config.Sprite);
            RenderDuration();
        }

        public void OnEnable()
        {
            _model.DurationUpdated += RenderDuration;
            _model.EffectEnded += OnEffectEnded;
        }

        private void OnDisable()
        {
            _model.DurationUpdated -= RenderDuration;
            _model.EffectEnded -= OnEffectEnded;
        }

        private void RenderDuration()
        {
            _view.RenderDuration(_model.Duration.ToString());
        }

        private void OnEffectEnded(AbilityEffect _)
        {
            OnDisable();
            Object.Destroy(_view.gameObject);
        }
    }
}