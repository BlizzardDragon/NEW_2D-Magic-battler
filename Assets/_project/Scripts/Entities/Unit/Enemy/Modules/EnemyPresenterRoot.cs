using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Compositions;
using _project.Scripts.Entities.Unit.Enemy.Compositions;
using _project.Scripts.Game.Commands;
using Entity.Core;
using UnityEngine;
using VampireSquid.Common.Commands.Handlers;

namespace _project.Scripts.Entities.Unit.Enemy.Modules
{
    public class EnemyPresenterRoot : EntityModuleCompositeRootBase
    {
        [SerializeField] private UnitMono _unitMono;

        private EnemyDeathPresenter _deathPresenter;

        public override void Create(IEntity entity)
        {
            var health = entity.GetModule<IHealth>();

            _deathPresenter = new EnemyDeathPresenter(health);
            CommandHandler.Instance.AddListener(_deathPresenter);

            entity.AddModule<UnitMono>(_unitMono);

            CreateComposition<UnitTargetComposition>(entity);
            CreateComposition<UnitAbilityComposition>(entity);
            CreateComposition<UnitDamageReceiverComposition>(entity);
            CreateComposition<UnitFinishGameComposition>(entity);
            CreateComposition<EnemyTurnComposition>(entity);

            CreateComposition<UnitViewComposition>(entity);
        }

        public override void Initialize()
        {
            _deathPresenter.OnEnable();
        }

        protected override void OnBeforeDestroy()
        {
            _deathPresenter.OnDisable();
        }

        public class EnemyDeathPresenter
        {
            private readonly IHealth _health;

            public EnemyDeathPresenter(IHealth health)
            {
                _health = health;
            }

            public void OnEnable()
            {
                _health.Died += OnDied;
            }

            public void OnDisable()
            {
                _health.Died -= OnDied;
            }

            private void OnDied()
            {
                CommandHandler.Instance.SendCommand(new WinGameCommand());
            }
        }
    }
}