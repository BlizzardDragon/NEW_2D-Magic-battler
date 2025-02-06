using _project.Scripts.Entities.Unit.Abilities.Effects;

namespace _project.Scripts.Entities.Unit.UI.AbilityEffects
{
    public class AbilityEffectsPresenter
    {
        private readonly IAbilityEffectsManager _abilityEffectsManager;
        private readonly IAbilityEffectViewFactory _viewFactory;

        public AbilityEffectsPresenter(
            IAbilityEffectsManager abilityEffectsManager,
            IAbilityEffectViewFactory viewFactory)
        {
            _abilityEffectsManager = abilityEffectsManager;
            _viewFactory = viewFactory;
        }

        public void OnEnable()
        {
            _abilityEffectsManager.EffectAdded += OnEffectAdded;
        }

        public void OnDisable()
        {
            _abilityEffectsManager.EffectAdded -= OnEffectAdded;
        }

        private void OnEffectAdded(AbilityEffect model)
        {
            var view = _viewFactory.CreateView();
            var presenter = new AbilityEffectPresenter(model, view);

            presenter.OnEnable();
        }
    }
}