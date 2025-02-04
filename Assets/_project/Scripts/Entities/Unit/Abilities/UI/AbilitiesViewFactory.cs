using System;
using System.Collections.Generic;
using _project.Scripts.Core.UI.Abilities;
using UnityEngine;

namespace _project.Scripts.Entities.Unit.Abilities.UI
{
    public class AbilitiesViewFactory : IDisposable
    {
        private readonly IAbilitiesHUDViewport _viewport;
        private readonly IAbilityManager _abilityManager;
        private readonly AbilityButtonView _buttonViewPrefab;

        private readonly List<AbilitiesViewPresenter> _presenters = new();

        public AbilitiesViewFactory(
            IAbilitiesHUDViewport viewport,
            IAbilityManager abilityManager,
            AbilityButtonView buttonViewPrefab)
        {
            _viewport = viewport;
            _abilityManager = abilityManager;
            _buttonViewPrefab = buttonViewPrefab;
        }

        public void CreateAbilitiesView()
        {
            foreach (var ability in _abilityManager.Abilities)
            {
                var view = GameObject.Instantiate(_buttonViewPrefab, _viewport.Content.transform);
                var presenter = new AbilitiesViewPresenter(ability, view);

                _presenters.Add(presenter);
                presenter.OnEnable();
            }
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
            {
                presenter.OnDisable();
            }
        }
    }
}