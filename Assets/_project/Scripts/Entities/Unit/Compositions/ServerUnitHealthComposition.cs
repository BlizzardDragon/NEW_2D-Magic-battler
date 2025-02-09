using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Network;
using _project.Scripts.Entities.Unit.Network.Health;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class ServerUnitHealthComposition : EntityModuleCompositionBase
    {
        private ServerHealthEventHandler _healthEventHandler;
        
        public override void Create(IEntity entity)
        {
            var unitMono = entity.GetModule<UnitMono>();

            var health = new EntityHealth(unitMono.UnitConfig.StartHealth);
            var networkHealthAdapter = new NetworkHealthAdapter();
            _healthEventHandler = new ServerHealthEventHandler(health, networkHealthAdapter);

            entity.AddModule<IHealth>(health);
            entity.AddModule<INetworkHealthAdapter>(networkHealthAdapter);
        }

        public override void Initialize()
        {
            _healthEventHandler.OnEnable();
        }

        protected override void OnBeforeDestroy()
        {
            _healthEventHandler.OnDisable();
        }
    }
}