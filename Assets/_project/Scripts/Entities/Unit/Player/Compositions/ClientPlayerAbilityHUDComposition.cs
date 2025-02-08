using _project.Scripts.Core.UI.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Network;
using _project.Scripts.Entities.Unit.Abilities.UI;
using _project.Scripts.Entities.Unit.Player.Configs;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Player.Compositions
{
    public class ClientPlayerAbilityHUDComposition : EntityModuleCompositionBase
    {
        private AbilitiesViewFactory _abilitiesViewFactory;

        public override void Create(IEntity entity)
        {
            var unitMono = entity.GetModule<UnitMono>();
            var uiConfig = entity.GetModule<PlayerUIConfig>();
            var networkAbilitiesAdapter = entity.GetModule<INetworkAbilitiesAdapter>();
            var abilitiesHudViewport = Get<IAbilitiesHUDViewport>();

            _abilitiesViewFactory = new AbilitiesViewFactory(
                abilitiesHudViewport, networkAbilitiesAdapter, unitMono.AbilitiesProvider,
                uiConfig.AbilityButtonViewPrefab);
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