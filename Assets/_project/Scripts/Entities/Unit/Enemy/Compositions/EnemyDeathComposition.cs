using _project.Scripts.Entities.Health;
using Entity.Core;
using VampireSquid.Common.Commands.Handlers;

namespace _project.Scripts.Entities.Unit.Enemy.Compositions
{
    public class EnemyDeathComposition : EntityModuleCompositionBase
    {
        private EnemyDeathPresenter _deathPresenter;

        public override void Create(IEntity entity)
        {
            var health = entity.GetModule<IHealth>();

            _deathPresenter = new EnemyDeathPresenter(health);

            CommandHandler.Instance.AddListener(_deathPresenter);
        }

        public override void Initialize()
        {
            _deathPresenter.OnEnable();
        }

        protected override void OnBeforeDestroy()
        {
            _deathPresenter.OnDisable();
        }
    }
}