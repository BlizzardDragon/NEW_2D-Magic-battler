namespace _project.Scripts.Entities.Unit.Abilities.Effects.Network
{
    public class ServerAbilityEffectsSyncHandler
    {
        private readonly INetworkAbilityEffectAdapter _networkAbilityEffectAdapter;
        private readonly IAbilityEffectsManager _abilityEffectsManager;

        public ServerAbilityEffectsSyncHandler(
            INetworkAbilityEffectAdapter networkAbilityEffectAdapter,
            IAbilityEffectsManager abilityEffectsManager)
        {
            _networkAbilityEffectAdapter = networkAbilityEffectAdapter;
            _abilityEffectsManager = abilityEffectsManager;
        }

        public void OnEnable()
        {
            _abilityEffectsManager.EffectAdded += OnEffectAdded;
            _abilityEffectsManager.EffectEnded += OnEffectEnded;

            foreach (var effect in _abilityEffectsManager.Effects)
            {
                effect.DurationUpdated += OnDurationUpdated;
            }
        }

        public void OnDisable()
        {
            _abilityEffectsManager.EffectAdded -= OnEffectAdded;
            _abilityEffectsManager.EffectEnded -= OnEffectEnded;

            foreach (var effect in _abilityEffectsManager.Effects)
            {
                effect.DurationUpdated -= OnDurationUpdated;
            }
        }

        private void OnEffectAdded(AbilityEffect abilityEffect)
        {
            _networkAbilityEffectAdapter.AddEffect_Server(abilityEffect.Type);
        }

        private void OnEffectEnded(AbilityEffect abilityEffect)
        {
            _networkAbilityEffectAdapter.RemoveEffect_Server(abilityEffect.Type);
        }

        private void OnDurationUpdated(AbilityEffect abilityEffect)
        {
            _networkAbilityEffectAdapter.UpdateDuration_Server(abilityEffect.Type, abilityEffect.Duration);
        }
    }
}