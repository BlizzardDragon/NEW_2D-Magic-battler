using _project.Scripts.Entities.Unit.Abilities.Configs;

namespace _project.Scripts.Entities.Unit.Abilities
{
    [AbilityType(AbilityType.Attack)]
    public class AttackAbility : Ability
    {
        private readonly IEntityTargetService _targetService;
        private readonly AttackAbilityConfig _config;

        public AttackAbility(IEntityTargetService targetService, AttackAbilityConfig config) : base(config)
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