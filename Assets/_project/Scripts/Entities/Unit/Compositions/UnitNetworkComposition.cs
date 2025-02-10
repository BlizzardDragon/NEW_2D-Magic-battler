using _project.Scripts.Entities.Unit.Abilities.Effects.Network;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class UnitNetworkComposition : EntityModuleCompositionBase
    {
        public override void Create(IEntity entity)
        {
            var networkAbilityEffectAdapter = new NetworkAbilityEffectAdapter();

            entity.AddModule<INetworkAbilityEffectAdapter>(networkAbilityEffectAdapter);
        }
    }
}