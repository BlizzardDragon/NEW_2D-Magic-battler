using _project.Scripts.Entities.Unit.Compositions;
using Entity.Core;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Modules
{
    public class PlayerPresenterRoot : EntityModuleCompositeRootBase
    {
        [SerializeField] private UnitMono _unitMono;

        public override void Create(IEntity entity)
        {
            entity.AddModule<UnitMono>(_unitMono);
            
            CreateComposition<PlayerAbilityComposition>(entity);
            CreateComposition<PlayerAbilityHUDComposition>(entity);
        }
    }
}