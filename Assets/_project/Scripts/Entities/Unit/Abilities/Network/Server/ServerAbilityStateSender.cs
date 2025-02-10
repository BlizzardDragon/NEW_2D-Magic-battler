namespace _project.Scripts.Entities.Unit.Abilities.Network.Server
{
    public interface IServerAbilityStateSender
    {
        void SendAbilityStateToClient(Ability ability);
        void SendAbilityEnableToClient(Ability ability, bool isEnable);
    }

    public class ServerAbilityStateSender : IServerAbilityStateSender
    {
        private readonly INetworkAbilitiesAdapter _networkAbilitiesAdapter;

        public ServerAbilityStateSender(INetworkAbilitiesAdapter networkAbilitiesAdapter)
        {
            _networkAbilitiesAdapter = networkAbilitiesAdapter;
        }

        public void SendAbilityStateToClient(Ability ability)
        {
            var type = ability.Config.Type;
            var isEnable = false;
            int? cooldown = null;

            if (!ability.IsEnable)
            {
                SendAbilityStateToServer(type, isEnable, cooldown);
                return;
            }

            if (ability.CooldownIsOver)
            {
                isEnable = true;
            }
            else
            {
                if (!ability.CooldownIsStopped)
                {
                    cooldown = ability.Cooldown;
                }
            }

            SendAbilityStateToServer(type, isEnable, cooldown);
        }

        private void SendAbilityStateToServer(AbilityType type, bool isEnable, int? cooldown)
        {
            _networkAbilitiesAdapter.UpdateAbilityEnable_Server(type, isEnable);
            _networkAbilitiesAdapter.UpdateAbilityCooldown_Server(type, cooldown);
        }

        public void SendAbilityEnableToClient(Ability ability, bool isEnable)
        {
            _networkAbilitiesAdapter.UpdateAbilityEnable_Server(ability.Config.Type, isEnable);
        }
    }
}