using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Configs;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class UnitAbilityComposition : EntityModuleCompositionBase
    {
        public override void Create(IEntity entity)
        {
            var health = entity.GetModule<IHealth>();
            var targetService = entity.GetModule<IEntityTargetService>();
            var unitMono = entity.GetModule<UnitMono>();
            var abilityConfigs = unitMono.AbilitiesProvider.AbilityConfigs;

            var abilityFactory = new AbilityFactory();
            var abilityManager = new AbilityManager();
            var effectsManager = new AbilityEffectsManager();

            AbilityFactoryRegistration(abilityFactory, targetService, health, effectsManager);

            foreach (var abilityConfig in abilityConfigs)
            {
                abilityManager.AddAbility(abilityFactory.CreateAbility(abilityConfig));
            }

            entity.AddModule<IAbilityEffectsManager>(effectsManager);
            entity.AddModule<IAbilityManager>(abilityManager);
        }

        private static void AbilityFactoryRegistration(
            IAbilityFactory abilityFactory,
            IEntityTargetService targetService,
            IHealth health,
            IAbilityEffectsManager effectsManager)
        {
            abilityFactory.Register(
                AbilityType.Attack,
                config => new AttackAbility(targetService, (AttackAbilityConfig) config));

            abilityFactory.Register(
                AbilityType.Barrier,
                config => new BarrierAbility(health, effectsManager, (BarrierAbilityConfig) config));

            abilityFactory.Register(
                AbilityType.Fireball,
                config => new FireballAbility(targetService, (FireballAbilityConfig) config));

            abilityFactory.Register(
                AbilityType.Purification,
                config => new PurificationAbility(effectsManager, (PurificationAbilityConfig) config));

            abilityFactory.Register(
                AbilityType.Regeneration,
                config => new RegenerationAbility(health, effectsManager, (RegenerationAbilityConfig) config));
        }
    }
}