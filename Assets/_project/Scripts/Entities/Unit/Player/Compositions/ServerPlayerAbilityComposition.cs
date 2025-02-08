using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Network;
using _project.Scripts.Entities.Unit.Abilities.Network.Server;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Player.Compositions
{
    public class ServerPlayerAbilityComposition : EntityModuleCompositionBase
    {
        private ServerAbilitiesSyncManager _syncManager;
        private ServerNetworkAbilityRequestsHandler _requestsHandler;

        public override void Create(IEntity entity)
        {
            var networkAbilitiesAdapter = entity.GetModule<INetworkAbilitiesAdapter>();
            var abilityManager = entity.GetModule<IAbilityManager>();

            var sender = new ServerToClientAbilityStateSender(networkAbilitiesAdapter);
            _syncManager = new ServerAbilitiesSyncManager(sender, abilityManager);
            _requestsHandler = new ServerNetworkAbilityRequestsHandler(
                networkAbilitiesAdapter, sender, abilityManager);
        }

        public override void Initialize()
        {
            _syncManager.OnEnable();
            _requestsHandler.OnEnable();
        }

        protected override void OnBeforeDestroy()
        {
            _syncManager.OnDisable();
            _requestsHandler.OnDisable();
        }
    }
}