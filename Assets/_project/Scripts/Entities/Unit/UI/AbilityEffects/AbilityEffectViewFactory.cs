using _project.Scripts.Entities.Unit.Configs;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.UI.AbilityEffects
{
    public interface IAbilityEffectViewFactory
    {
        AbilityEffectView CreateView();
    }

    public class AbilityEffectViewFactory : IAbilityEffectViewFactory
    {
        private readonly Transform _effectParent;
        private readonly UnitUIConfig _uiConfig;

        public AbilityEffectViewFactory(Transform effectParent, UnitUIConfig uiConfig)
        {
            _effectParent = effectParent;
            _uiConfig = uiConfig;
        }

        public AbilityEffectView CreateView()
        {
            return GameObject.Instantiate(_uiConfig.AbilityEffectViewPrefab, _effectParent);
        }
    }
}