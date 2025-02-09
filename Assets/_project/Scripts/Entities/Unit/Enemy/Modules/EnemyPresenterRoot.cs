using _project.Scripts.Entities.Unit.Compositions;
using _project.Scripts.Entities.Unit.Enemy.Compositions;
using Entity.Core;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Enemy.Modules
{
    public class EnemyPresenterRoot : EntityModuleCompositeRootBase
    {
        [SerializeField] private UnitMono _unitMono;

        public override void Create(IEntity entity)
        {
            entity.AddModule<UnitMono>(_unitMono);

            CreateComposition<ServerUnitHealthComposition>(entity);
            CreateComposition<ServerUnitTargetComposition>(entity);
            CreateComposition<ServerUnitAbilityComposition>(entity);
            CreateComposition<ServerUnitTakeDamageComposition>(entity);
            CreateComposition<ServerUnitFinishGameComposition>(entity);
            CreateComposition<EnemyTurnComposition>(entity);
            CreateComposition<EnemyDeathComposition>(entity);

            CreateComposition<UnitViewComposition>(entity);
        }
    }
}