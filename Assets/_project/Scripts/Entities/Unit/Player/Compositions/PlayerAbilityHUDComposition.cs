using _project.Scripts.Core.UI.Abilities;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.UI;
using _project.Scripts.Entities.Unit.Player.Configs;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Player.Compositions
{
    public class PlayerAbilityHUDComposition : EntityModuleCompositionBase
    {
        private AbilitiesViewFactory _abilitiesViewFactory;

        public override void Create(IEntity entity)
        {
            var uiConfig = entity.GetModule<PlayerUIConfig>();
            var abilityManager = entity.GetModule<IAbilityManager>();
            var abilitiesHudViewport = Get<IAbilitiesHUDViewport>();

            _abilitiesViewFactory = new AbilitiesViewFactory(
                abilitiesHudViewport, abilityManager, uiConfig.AbilityButtonViewPrefab);
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