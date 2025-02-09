using _project.Scripts.Entities.Unit.Abilities.Effects;
using _project.Scripts.Entities.Unit.Network;
using _project.Scripts.Entities.Unit.Network.Health;
using _project.Scripts.Entities.Unit.UI.AbilityEffects;
using _project.Scripts.Entities.Unit.UI.HealthBar;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class UnitViewComposition : EntityModuleCompositionBase
    {
        private HealthBarViewPresenter _healthBarViewPresenter;
        private AbilityEffectsPresenter _abilityEffectsPresenter;

        public override void Create(IEntity entity)
        {
            var unitMono = entity.GetModule<UnitMono>();
            var networkHealthAdapter = entity.GetModule<INetworkHealthAdapter>();
            var abilityEffectsManager = entity.GetModule<IAbilityEffectsManager>();

            _healthBarViewPresenter = new HealthBarViewPresenter(networkHealthAdapter, unitMono.HealthBarView);

            var abilityEffectViewFactory = new AbilityEffectViewFactory(
                unitMono.StatusBarContent, unitMono.UIConfig);

            _abilityEffectsPresenter = new AbilityEffectsPresenter(abilityEffectsManager, abilityEffectViewFactory);
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