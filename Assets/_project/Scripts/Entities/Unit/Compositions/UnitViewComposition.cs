using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Effects;
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
            var health = entity.GetModule<IHealth>();
            var abilityEffectsManager = entity.GetModule<IAbilityEffectsManager>();

            _healthBarViewPresenter = new HealthBarViewPresenter(health, unitMono.HealthBarView);

            var abilityEffectViewFactory = new AbilityEffectViewFactory(
                unitMono.StatusBarContent, unitMono.UnitUIConfig);

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