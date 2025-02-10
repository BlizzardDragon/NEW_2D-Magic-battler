using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Network;
using _project.Scripts.Entities.Unit.Abilities.Network.Server;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Player.Compositions
{
    public class ServerPlayerAbilityComposition : EntityModuleCompositionBase
    {
        private ServerAbilitiesSyncController _syncController;
        private ServerAbilityRequestsHandler _requestsHandler;

        public override void Create(IEntity entity)
        {
            var networkAbilitiesAdapter = entity.GetModule<INetworkAbilitiesAdapter>();
            var abilityManager = entity.GetModule<IAbilityManager>();

            var sender = new ServerAbilityStateSender(networkAbilitiesAdapter);
            _syncController = new ServerAbilitiesSyncController(sender, abilityManager);
            _requestsHandler = new ServerAbilityRequestsHandler(
                networkAbilitiesAdapter, sender, abilityManager);
        }

        public override void Initialize()
        {
            _syncController.OnEnable();
            _requestsHandler.OnEnable();
        }

        protected override void OnBeforeDestroy()
        {
            _syncController.OnDisable();
            _requestsHandler.OnDisable();
        }
    }
}