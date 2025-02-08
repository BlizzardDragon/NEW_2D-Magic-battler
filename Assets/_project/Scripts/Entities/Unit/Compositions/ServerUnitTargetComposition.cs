using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class ServerUnitTargetComposition : EntityModuleCompositionBase
    {
        public override void Create(IEntity entity)
        {
            var unitMono = entity.GetModule<UnitMono>();

            var targetService = new EntityTargetService();
            targetService.SetTarget(unitMono.Target);

            entity.AddModule<IEntityTargetService>(targetService);
        }
    }
}