using _project.Scripts.Entities.Health;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using _project.Scripts.Game;
using Entity.Core;

namespace _project.Scripts.Entities.Unit.Compositions
{
    public class ServerUnitTakeDamageComposition : EntityModuleCompositionBase
    {
        private UnitDeathPresenter _deathPresenter;

        public override void Create(IEntity entity)
        {
            var health = entity.GetModule<IHealth>();
            var effectsManager = entity.GetModule<IAbilityEffectsManager>();
            var turnPipelineRunner = GetLocal<ITurnPipelineRunner>();

            var damageReceiver = new UnitDamageReceiver(health, effectsManager);
            _deathPresenter = new UnitDeathPresenter(health, turnPipelineRunner);

            entity.AddModule<IDamageReceiver>(damageReceiver);
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