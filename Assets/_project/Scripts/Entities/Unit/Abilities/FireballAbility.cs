using _project.Scripts.Entities.Unit.Abilities.Configs;

namespace _project.Scripts.Entities.Unit.Abilities
{
    public class FireballAbility : Ability
    {
        private readonly IEntityTargetService _targetService;
        private readonly FireballAbilityConfig _config;

        public FireballAbility(IEntityTargetService targetService, FireballAbilityConfig config) : base(config)
        {
            _targetService = targetService;
            _config = config;
        }

        protected override void OnUse()
        {
            var target = _targetService.Target;

            if (target.TryGetModule(out IDamageReceiver damageReceiver))
            {
                damageReceiver.TakeDamage(_config.Damage);
            }
        }
    }
}