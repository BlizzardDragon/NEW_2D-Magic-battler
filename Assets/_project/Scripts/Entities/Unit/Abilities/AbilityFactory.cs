using System;
using System.Collections.Generic;
using _project.Scripts.Entities.Unit.Abilities.Configs;

namespace _project.Scripts.Entities.Unit.Abilities
{
    public interface IAbilityFactory
    {
        Ability CreateAbility(AbilityConfig config);
        void Register(AbilityType type, Func<AbilityConfig, Ability> creator);
    }

    public class AbilityFactory : IAbilityFactory
    {
        private readonly Dictionary<AbilityType, Func<AbilityConfig, Ability>> _creators = new();

        public Ability CreateAbility(AbilityConfig config)
        {
            if (_creators.TryGetValue(config.Type, out var creator))
            {
                return creator(config);
            }

            throw new Exception($"Unknown ability type: {config.Type}");
        }

        public void Register(AbilityType type, Func<AbilityConfig, Ability> creator)
        {
            _creators[type] = creator;
        }
    }
}