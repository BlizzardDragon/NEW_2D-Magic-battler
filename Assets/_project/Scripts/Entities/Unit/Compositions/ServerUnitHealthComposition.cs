using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Network;
using _project.Scripts.Entities.Unit.Network.Health;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class ServerUnitHealthComposition : EntityModuleCompositionBase
    {
        private ServerHealthSyncHandler _healthSyncHandler;
        
        public override void Create(IEntity entity)
        {
            var unitMono = entity.GetModule<UnitMono>();

            var health = new EntityHealth(unitMono.UnitConfig.StartHealth);
            var networkHealthAdapter = new NetworkHealthAdapter();
            _healthSyncHandler = new ServerHealthSyncHandler(health, networkHealthAdapter);

            entity.AddModule<IHealth>(health);
            entity.AddModule<INetworkHealthAdapter>(networkHealthAdapter);
        }

        public override void Initialize()
        {
            _healthSyncHandler.OnEnable();
        }

        protected override void OnBeforeDestroy()
        {
            _healthSyncHandler.OnDisable();
        }
    }
}