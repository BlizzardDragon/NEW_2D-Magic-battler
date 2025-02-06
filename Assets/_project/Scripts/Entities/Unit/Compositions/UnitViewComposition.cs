using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.UI;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class UnitViewComposition : EntityModuleCompositionBase
    {
        private HealthBarViewPresenter _healthBarViewPresenter;

        public override void Create(IEntity entity)
        {
            var health = entity.GetModule<IHealth>();
            var unitMono = entity.GetModule<UnitMono>();

            _healthBarViewPresenter = new HealthBarViewPresenter(health, unitMono.HealthBarView);
        }

        public override void Initialize()
        {
            _healthBarViewPresenter.OnEnable();
        }

        protected override void OnBeforeDestroy()
        {
            _healthBarViewPresenter.OnDisable();
        }
    }
}