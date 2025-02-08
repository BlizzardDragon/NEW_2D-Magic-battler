namespace _project.Scripts.Entities.Unit.Abilities.Network.Server
{
    public class ServerAbilityEventHandler
    {
        private readonly Ability _ability;
        private readonly IServerToClientAbilityStateSender _sender;

        public ServerAbilityEventHandler(
            Ability ability,
            IServerToClientAbilityStateSender sender)
        {
            _ability = ability;
            _sender = sender;
        }

        public void OnEnable()
        {
            _ability.CooldownUpdated += OnCooldownUpdated;
            _ability.Enabled += OnAbilityEnabled;
        }

        public void OnDisable()
        {
            _ability.CooldownUpdated -= OnCooldownUpdated;
            _ability.Enabled -= OnAbilityEnabled;
        }

        private void OnCooldownUpdated()
        {
            _sender.SendAbilityStateToClient(_ability);
        }

        private void OnAbilityEnabled(bool enable)
        {
            _sender.SendAbilityEnableToClient(_ability, enable);
        }
    }
}