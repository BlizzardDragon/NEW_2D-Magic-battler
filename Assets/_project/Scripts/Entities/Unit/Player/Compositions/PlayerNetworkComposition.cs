using _project.Scripts.Entities.Unit.Abilities.Network;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Player.Compositions
{
    public class PlayerNetworkComposition : EntityModuleCompositionBase
    {
        public override void Create(IEntity entity)
        {
            var networkAbilitiesAdapter = new NetworkAbilitiesAdapter();

            entity.AddModule<INetworkAbilitiesAdapter>(networkAbilitiesAdapter);
        }
    }
}