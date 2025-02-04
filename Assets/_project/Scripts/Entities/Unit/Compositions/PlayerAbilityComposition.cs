using _project.Scripts.Entities.Unit.Abilities;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class PlayerAbilityComposition : EntityModuleCompositionBase
    {
        public override void Create(IEntity entity)
        {
            var unitMono = entity.GetModule<UnitMono>();
            var abilityConfigs = unitMono.AbilitiesProvider.AbilityConfigs;

            var abilityFactory = new AbilityFactory();
            var abilityManager = new AbilityManager();

            foreach (var abilityConfig in abilityConfigs)
            {
                abilityManager.AddAbility(abilityFactory.CreateAbility(abilityConfig));
            }

            entity.AddModule<IAbilityManager>(abilityManager);
        }
    }
}