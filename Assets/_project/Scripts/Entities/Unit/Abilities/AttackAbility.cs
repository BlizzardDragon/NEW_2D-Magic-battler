using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Configs;

namespace _project.Scripts.Entities.Unit.Abilities
{
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

            if (target.TryGetModule(out IHealth health))
            {
                health.TakeDamage(_config.Damage);
            }
        }
    }
}