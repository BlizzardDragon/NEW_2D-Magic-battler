using _project.Scripts.Entities.Unit.Compositions;
using _project.Scripts.Entities.Unit.Player.Compositions;
using _project.Scripts.Entities.Unit.Player.Configs;
using Entity.Core;
using UnityEngine;
using VampireSquid.Common.Connections;

namespace _project.Scripts.Entities.Unit.Player.Modules
{
    public class PlayerPresenterRoot : EntityModuleCompositeRootBase
    {
        [SerializeField] private UnitMono _unitMono;
        [SerializeField] private PlayerUIConfig _uiConfig;

        public override void Create(IEntity entity)
        {
            entity.AddModule<UnitMono>(_unitMono);
            entity.AddModule<PlayerUIConfig>(_uiConfig);

            CreateComposition<PlayerNetworkComposition>(entity);

            if (entity.Presence.OnServer())
            {
                CreateComposition<UnitTargetComposition>(entity);
                CreateComposition<UnitAbilityComposition>(entity);
                
                CreateComposition<ServerPlayerAbilityComposition>(entity);
            }

            if (entity.Presence.IsLocal())
            {
            }

            CreateComposition<UnitTakeDamageComposition>(entity);
            CreateComposition<UnitFinishGameComposition>(entity);
            CreateComposition<PlayerTurnComposition>(entity);
            CreateComposition<PlayerDeathComposition>(entity);

            CreateComposition<UnitViewComposition>(entity);
            CreateComposition<ClientPlayerAbilityHUDComposition>(entity);
        }
    }
}