namespace _project.Scripts.Entities.Unit.Abilities
{
    public abstract class Ability
    {
        protected Ability(int cooldown)
        {
            Cooldown = cooldown;
        }

        private int Cooldown { get; }

        public abstract void UseAbility();
    }
}