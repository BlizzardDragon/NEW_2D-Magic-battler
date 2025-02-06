using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using UnityEngine;

namespace _project.Scripts.Core.Turn.Tasks
{
    public class PlayerTurnTask : Task
    {
        private readonly IAbilityManager _abilityManager;
        private readonly IAbilityEffectsManager _abilityEffectsManager;

        public PlayerTurnTask(IAbilityManager abilityManager, IAbilityEffectsManager abilityEffectsManager)
        {
            _abilityManager = abilityManager;
            _abilityEffectsManager = abilityEffectsManager;
        }

        protected override void OnRun()
        {
            _abilityManager.EnableAbilities(true);

            foreach (var ability in _abilityManager.Abilities)
            {
                ability.Used += OnUsed;
            }

            _abilityManager.TickCooldown();
            _abilityEffectsManager.Tick();
        }

        protected override void OnFinish()
        {
            _abilityManager.EnableAbilities(false);

            foreach (var ability in _abilityManager.Abilities)
            {
                ability.Used -= OnUsed;
            }
        }

        private void OnUsed(Ability ability)
        {
            Debug.Log($"Player used: {ability.Name}");
            Finish();
        }
    }
}