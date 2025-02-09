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
                CreateComposition<ServerUnitHealthComposition>(entity);
                CreateComposition<ServerUnitTargetComposition>(entity);
                CreateComposition<ServerUnitAbilityComposition>(entity);
                CreateComposition<ServerUnitTakeDamageComposition>(entity);
                CreateComposition<ServerUnitFinishGameComposition>(entity);

                CreateComposition<ServerPlayerTurnComposition>(entity);
                CreateComposition<ServerPlayerDeathComposition>(entity);
                CreateComposition<ServerPlayerAbilityComposition>(entity);
            }

            if (entity.Presence.IsRemote())
            {
                CreateComposition<UnitViewComposition>(entity);
                CreateComposition<ClientPlayerAbilityHUDComposition>(entity);
            }
        }
    }
}