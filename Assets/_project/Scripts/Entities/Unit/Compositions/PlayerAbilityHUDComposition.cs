using _project.Scripts.Core.UI.Abilities;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.UI;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class PlayerAbilityHUDComposition : EntityModuleCompositionBase
    {
        private AbilitiesViewFactory _abilitiesViewFactory;

        public override void Create(IEntity entity)
        {
            var unitMono = entity.GetModule<UnitMono>();
            var abilityManager = entity.GetModule<IAbilityManager>();
            var abilitiesHudViewport = GetGlobal<AbilitiesHUDViewport>();

            _abilitiesViewFactory = new AbilitiesViewFactory(
                abilitiesHudViewport, abilityManager, unitMono.AbilityButtonViewPrefab);
        }

        public override void Initialize()
        {
            _abilitiesViewFactory.CreateAbilitiesView();
        }

        protected override void OnBeforeDestroy()
        {
            _abilitiesViewFactory.Dispose();
        }
    }
}