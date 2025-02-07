namespace _project.Scripts.Entities.Unit.Abilities.Network.Server
{
    public class ServerUseAbilityRequestHandler
    {
        private readonly INetworkAbilitiesAdapter _networkAbilitiesAdapter;
        private readonly IServerToClientAbilityStateSender _serverToClientAbilityStateSender;
        private readonly IAbilityManager _abilityManager;

        public ServerUseAbilityRequestHandler(
            INetworkAbilitiesAdapter networkAbilitiesAdapter,
            IServerToClientAbilityStateSender serverToClientAbilityStateSender,
            IAbilityManager abilityManager)
        {
            _networkAbilitiesAdapter = networkAbilitiesAdapter;
            _serverToClientAbilityStateSender = serverToClientAbilityStateSender;
            _abilityManager = abilityManager;
        }

        public void OnEnable()
        {
            _networkAbilitiesAdapter.ClientRequestedAbilityUse += OnClientRequestedAbilityUse;
        }

        public void OnDisable()
        {
            _networkAbilitiesAdapter.ClientRequestedAbilityUse -= OnClientRequestedAbilityUse;
        }

        private void OnClientRequestedAbilityUse(AbilityType type)
        {
            var ability = _abilityManager.GetAbility(type);

            ability.Use();
            _serverToClientAbilityStateSender.SendAbilityStateToClient(ability);
        }
    }
}