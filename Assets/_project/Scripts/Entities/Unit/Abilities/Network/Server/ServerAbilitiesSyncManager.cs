using System.Collections.Generic;
using _project.Scripts.Entities.Unit.Abilities.UI;

namespace _project.Scripts.Entities.Unit.Abilities.Network.Server
{
    public class ServerAbilitiesSyncManager
    {
        private readonly IServerToClientAbilityStateSender _sender;
        private readonly IAbilityManager _abilityManager;

        private readonly List<ServerAbilityEventHandler> _handlers = new();

        public ServerAbilitiesSyncManager(IServerToClientAbilityStateSender sender, IAbilityManager abilityManager)
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