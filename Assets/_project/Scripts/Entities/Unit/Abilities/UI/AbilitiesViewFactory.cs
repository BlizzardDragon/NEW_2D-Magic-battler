using System;
using System.Collections.Generic;
using _project.Scripts.Core.UI.Abilities;
using _project.Scripts.Entities.Unit.Abilities.Configs;
using _project.Scripts.Entities.Unit.Abilities.Network;
using Object = UnityEngine.Object;

namespace _project.Scripts.Entities.Unit.Abilities.UI
{
    public class AbilitiesViewFactory : IDisposable
    {
        private readonly IAbilitiesHUDViewport _viewport;
        private readonly INetworkAbilitiesAdapter _networkAbilitiesAdapter;
        private readonly AbilitiesProvider _abilitiesProvider;
        private readonly AbilityButtonView _buttonViewPrefab;

        private readonly List<AbilityViewPresenter> _presenters = new();

        public AbilitiesViewFactory(
            IAbilitiesHUDViewport viewport,
            INetworkAbilitiesAdapter networkAbilitiesAdapter,
            AbilitiesProvider abilitiesProvider,
            AbilityButtonView buttonViewPrefab)
        {
            _viewport = viewport;
            _networkAbilitiesAdapter = networkAbilitiesAdapter;
            _abilitiesProvider = abilitiesProvider;
            _buttonViewPrefab = buttonViewPrefab;
        }
        
        public void CreateAbilitiesView()
        {
            foreach (var config in _abilitiesProvider.AbilityConfigs)
            {
                var view = Object.Instantiate(_buttonViewPrefab, _viewport.Content.transform);
                var presenter = new AbilityViewPresenter(view, _networkAbilitiesAdapter, config);

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