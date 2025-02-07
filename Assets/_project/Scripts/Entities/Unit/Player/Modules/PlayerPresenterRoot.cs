using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Compositions;
using _project.Scripts.Entities.Unit.Player.Compositions;
using _project.Scripts.Entities.Unit.Player.Configs;
using _project.Scripts.Game.Commands;
using Entity.Core;
using UnityEngine;
using VampireSquid.Common.Commands.Handlers;

namespace _project.Scripts.Entities.Unit.Player.Modules
{
    public class PlayerPresenterRoot : EntityModuleCompositeRootBase
    {
        [SerializeField] private UnitMono _unitMono;
        [SerializeField] private PlayerUIConfig _uiConfig;

        private PlayerDeathPresenter _deathPresenter;

        public override void Create(IEntity entity)
        {
            var health = entity.GetModule<IHealth>();

            _deathPresenter = new PlayerDeathPresenter(health);
            CommandHandler.Instance.AddListener(_deathPresenter);
            
            entity.AddModule<UnitMono>(_unitMono);
            entity.AddModule<PlayerUIConfig>(_uiConfig);

            CreateComposition<UnitTargetComposition>(entity);
            CreateComposition<UnitAbilityComposition>(entity);
            CreateComposition<UnitDamageReceiverComposition>(entity);
            CreateComposition<UnitFinishGameComposition>(entity);
            CreateComposition<PlayerTurnComposition>(entity);

            CreateComposition<UnitViewComposition>(entity);
            CreateComposition<PlayerAbilityHUDComposition>(entity);
        }

        public override void Initialize()
        {
            _deathPresenter.OnEnable();
        }

        protected override void OnBeforeDestroy()
        {
            _deathPresenter.OnDisable();
        }

        private class PlayerDeathPresenter
        {
            private readonly IHealth _health;

            public PlayerDeathPresenter(IHealth health)
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
                CommandHandler.Instance.SendCommand(new LoseGameCommand());
            }
        }
    }
}