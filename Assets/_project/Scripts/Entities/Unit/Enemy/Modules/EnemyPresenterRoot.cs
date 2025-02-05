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
            
            CreateComposition<UnitTargetComposition>(entity);
            CreateComposition<UnitAbilityComposition>(entity);
            CreateComposition<UnitDamageReceiverComposition>(entity);
            CreateComposition<EnemyTurnComposition>(entity);
        }
    }
}