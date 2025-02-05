using _project.Scripts.Core.UI.Abilities;
using _project.Scripts.Entities.Unit.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Effects;
using UnityEngine;

namespace _project.Scripts.Core.Turn.Tasks
{
    public class PlayerTurnTask : Task
    {
        private readonly IAbilityManager _abilityManager;
        private readonly IAbilityEffectsManager _abilityEffectsManager;
        private readonly IAbilitiesHUDViewport _abilitiesHudViewport;

        public PlayerTurnTask(
            IAbilityManager abilityManager,
            IAbilityEffectsManager abilityEffectsManager,
            IAbilitiesHUDViewport abilitiesHudViewport)
        {
            _abilityManager = abilityManager;
            _abilityEffectsManager = abilityEffectsManager;
            _abilitiesHudViewport = abilitiesHudViewport;
        }

        protected override void OnRun()
        {
            foreach (var ability in _abilityManager.Abilities)
            {
                ability.Used += OnUsed; 
            }

            _abilityManager.TickCooldown();
            _abilityEffectsManager.Tick();

            _abilitiesHudViewport.Show();
        }

        protected override void OnFinish()
        {
            _abilitiesHudViewport.Hide();

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