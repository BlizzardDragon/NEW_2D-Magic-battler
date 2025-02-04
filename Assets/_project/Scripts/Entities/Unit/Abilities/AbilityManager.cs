using System.Collections.Generic;

namespace _project.Scripts.Entities.Unit.Abilities
{
    public interface IAbilityManager
    {
        IReadOnlyList<Ability> Abilities { get; }

        void TickCooldown();
        void AddAbility(Ability ability);
    }

    public class AbilityManager : IAbilityManager
    {
        private readonly List<Ability> _abilities = new();

        public IReadOnlyList<Ability> Abilities => _abilities;

        public void TickCooldown()
        {
            foreach (var ability in _abilities)
            {
                ability.TickCooldown();
            }
        }

        public void AddAbility(Ability ability)
        {
            _abilities.Add(ability);
        }
    }
}