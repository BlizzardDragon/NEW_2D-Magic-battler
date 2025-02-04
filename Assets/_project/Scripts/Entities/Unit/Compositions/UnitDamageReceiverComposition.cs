using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class UnitDamageReceiverComposition : EntityModuleCompositionBase
    {
        public override void Create(IEntity entity)
        {
            var health = entity.GetModule<IHealth>();
            var effectsManager = entity.GetModule<IAbilityEffectsManager>();

            var damageReceiver = new UnitDamageReceiver(health, effectsManager);

            entity.AddModule<IDamageReceiver>(damageReceiver);
        }
    }
}