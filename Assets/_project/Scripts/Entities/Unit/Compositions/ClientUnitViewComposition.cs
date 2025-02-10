using _project.Scripts.Entities.Unit.Abilities.Effects.Network;
using _project.Scripts.Entities.Unit.Network.Health;
using _project.Scripts.Entities.Unit.UI.AbilityEffects;
using _project.Scripts.Entities.Unit.UI.HealthBar;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class ClientUnitViewComposition : EntityModuleCompositionBase
    {
        private HealthBarViewPresenter _healthBarViewPresenter;
        private AbilityEffectsPresenter _abilityEffectsPresenter;

        public override void Create(IEntity entity)
        {
            var unitMono = entity.GetModule<UnitMono>();
            var networkHealthAdapter = entity.GetModule<INetworkHealthAdapter>();
            var networkAbilityEffectAdapter = entity.GetModule<INetworkAbilityEffectAdapter>();

            _healthBarViewPresenter = new HealthBarViewPresenter(networkHealthAdapter, unitMono.HealthBarView);

            var abilityEffectViewFactory = new AbilityEffectViewFactory(
                unitMono.StatusBarContent, unitMono.UIConfig);

            _abilityEffectsPresenter = new AbilityEffectsPresenter(
                networkAbilityEffectAdapter, abilityEffectViewFactory, unitMono.AbilityEffectsProvider);
        }

        public override void Initialize()
        {
            _healthBarViewPresenter.OnEnable();
            _abilityEffectsPresenter.OnEnable();
        }

        protected override void OnBeforeDestroy()
        {
            _healthBarViewPresenter.OnDisable();
            _abilityEffectsPresenter.OnDisable();
        }
    }
}