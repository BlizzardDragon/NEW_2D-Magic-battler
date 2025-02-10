using System.Collections.Generic;

namespace _project.Scripts.Entities.Unit.Abilities.Network.Server
{
    public class ServerAbilitiesSyncController
    {
        private readonly IServerAbilityStateSender _sender;
        private readonly IAbilityManager _abilityManager;

        private readonly List<ServerAbilityEventHandler> _handlers = new();

        public ServerAbilitiesSyncController(IServerAbilityStateSender sender, IAbilityManager abilityManager)
        {
            _sender = sender;
            _abilityManager = abilityManager;
        }

        public void OnEnable()
        {
            foreach (var ability in _abilityManager.Abilities)
            {
                _sender.SendAbilityStateToClient(ability);

                var handler = new ServerAbilityEventHandler(ability, _sender);
                _handlers.Add(handler);

                handler.OnEnable();
            }
        }

        public void OnDisable()
        {
            foreach (var handler in _handlers)
            {
                handler.OnDisable();
            }
        }
    }
}