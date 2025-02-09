using System;

namespace _project.Scripts.Entities.Unit.Abilities.Effects.Network
{
    public interface INetworkAbilityEffectAdapter
    {
        event Action<AbilityEffectType> ServerEffectAdded;
        event Action<AbilityEffectType> ServerEffectEnded;
        event Action<AbilityEffectType, int> ServerDurationUpdated;

        void AddEffect_Server(AbilityEffectType type);
        void RemoveEffect_Server(AbilityEffectType type);
        void UpdateDuration_Server(AbilityEffectType type, int duration);
    }

    public class NetworkAbilityEffectAdapter : INetworkAbilityEffectAdapter
    {
        public event Action<AbilityEffectType> ServerEffectAdded;
        public event Action<AbilityEffectType> ServerEffectEnded;
        public event Action<AbilityEffectType, int> ServerDurationUpdated;

        public void AddEffect_Server(AbilityEffectType type)
        {
            AddEffect_Client(type);
        }

        private void AddEffect_Client(AbilityEffectType type)
        {
            ServerEffectAdded?.Invoke(type);
        }

        public void RemoveEffect_Server(AbilityEffectType type)
        {
            RemoveEffect_Client(type);
        }

        private void RemoveEffect_Client(AbilityEffectType type)
        {
            ServerEffectEnded?.Invoke(type);
        }

        public void UpdateDuration_Server(AbilityEffectType type, int duration)
        {
            UpdateDuration_Client(type, duration);
        }

        private void UpdateDuration_Client(AbilityEffectType type, int duration)
        {
            ServerDurationUpdated?.Invoke(type, duration);
        }
    }
}