namespace _project.Scripts.Entities.Unit.Abilities
{
    public class AttackAbility : Ability
    {
        private readonly IEntityTargetService _targetService;

        public AttackAbility(int cooldown, IEntityTargetService targetService) : base(cooldown)
        {
            _targetService = targetService;
        }

        public override void UseAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}