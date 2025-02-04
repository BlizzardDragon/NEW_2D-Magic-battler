using System;

namespace _project.Scripts.Entities.Unit.Abilities
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AbilityTypeAttribute : Attribute
    {
        public AbilityTypeAttribute(AbilityType type)
        {
            Type = type;
        }

        public AbilityType Type { get; }
    }
}