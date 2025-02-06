using _project.Scripts.Entities.Unit.Compositions;
using _project.Scripts.Entities.Unit.Player.Compositions;
using _project.Scripts.Entities.Unit.Player.Configs;
using Entity.Core;
using UnityEngine;

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

            CreateComposition<UnitTargetComposition>(entity);
            CreateComposition<UnitAbilityComposition>(entity);
            CreateComposition<UnitDamageReceiverComposition>(entity);
            CreateComposition<PlayerTurnComposition>(entity);

            CreateComposition<UnitViewComposition>(entity);
            CreateComposition<PlayerAbilityHUDComposition>(entity);
        }
    }
}