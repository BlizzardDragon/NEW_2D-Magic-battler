using _project.Scripts.Entities.Unit.Abilities.Effects;
using _project.Scripts.Entities.Unit.Abilities.Effects.Configs;
using _project.Scripts.Entities.Unit.Abilities.Effects.Network;

namespace _project.Scripts.Entities.Unit.UI.AbilityEffects
{
    public class AbilityEffectsPresenter
    {
        private readonly INetworkAbilityEffectAdapter _networkAbilityEffectAdapter;
        private readonly IAbilityEffectViewFactory _viewFactory;
        private readonly AbilityEffectsProvider _abilityEffectsProvider;

        public AbilityEffectsPresenter(
            INetworkAbilityEffectAdapter networkAbilityEffectAdapter,
            IAbilityEffectViewFactory viewFactory,
            AbilityEffectsProvider abilityEffectsProvider)
        {
            _networkAbilityEffectAdapter = networkAbilityEffectAdapter;
            _viewFactory = viewFactory;
            _abilityEffectsProvider = abilityEffectsProvider;
        }

        public void OnEnable()
        {
            _networkAbilityEffectAdapter.ServerEffectAdded += OnServerEffectAdded;
        }

        public void OnDisable()
        {
            _networkAbilityEffectAdapter.ServerEffectAdded -= OnServerEffectAdded;
        }

        private void OnServerEffectAdded(AbilityEffectType type)
        {
            var view = _viewFactory.CreateView();
            var config = _abilityEffectsProvider.GetConfig(type);
            var presenter = new AbilityEffectPresenter(_networkAbilityEffectAdapter, view, config);

            presenter.OnEnable();
        }
    }
}