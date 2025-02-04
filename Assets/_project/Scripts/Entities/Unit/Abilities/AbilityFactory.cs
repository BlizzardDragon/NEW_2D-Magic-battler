using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _project.Scripts.Entities.Unit.Abilities.Configs;

namespace _project.Scripts.Entities.Unit.Abilities
{
    public class AbilityFactory
    {
        private readonly Dictionary<AbilityType, Func<AbilityConfig, Ability>> _creators = new();

        public AbilityFactory()
        {
            RegisterAllAbilities();
        }

        public Ability CreateAbility(AbilityConfig config)
        {
            if (_creators.TryGetValue(config.Type, out var creator))
            {
                return creator(config);
            }

            throw new Exception($"Unknown ability type: {config.Type}");
        }

        private void RegisterAllAbilities()
        {
            var abilityTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Ability)))
                .Select(t => new
                {
                    Type = t,
                    Attribute = t.GetCustomAttribute<AbilityTypeAttribute>()
                })
                .Where(x => x.Attribute != null);

            foreach (var entry in abilityTypes)
            {
                _creators[entry.Attribute!.Type] = config =>
                    (Ability) Activator.CreateInstance(entry.Type, config)!;
            }
        }
    }
}