using _project.Scripts.Entities.Unit.Compositions;
using _project.Scripts.Entities.Unit.Enemy.Compositions;
using Entity.Core;
using UnityEngine;
using VampireSquid.Common.Connections;

namespace _project.Scripts.Entities.Unit.Enemy.Modules
{
    public class EnemyPresenterRoot : EntityModuleCompositeRootBase
    {
        [SerializeField] private UnitMono _unitMono;

        public override void Create(IEntity entity)
        {
            entity.AddModule<UnitMono>(_unitMono);

            CreateComposition<UnitNetworkComposition>(entity);

            if (entity.Presence.OnServer())
            {
                CreateComposition<ServerUnitHealthComposition>(entity);
                CreateComposition<ServerUnitTargetComposition>(entity);
                CreateComposition<ServerUnitAbilityComposition>(entity);
                CreateComposition<ServerUnitTakeDamageComposition>(entity);
                CreateComposition<ServerUnitFinishGameComposition>(entity);
                
                CreateComposition<ServerEnemyTurnComposition>(entity);
                CreateComposition<ServerEnemyDeathComposition>(entity);
            }

            if (entity.Presence.IsRemote())
            {
                CreateComposition<ClientUnitViewComposition>(entity);
            }
        }
    }
}