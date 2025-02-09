using _project.Scripts.Entities.Unit.Network.Health;

namespace _project.Scripts.Entities.Unit.UI.HealthBar
{
    public class HealthBarViewPresenter
    {
        private readonly INetworkHealthAdapter _networkHealthAdapter;
        private readonly HealthBarView _view;

        public HealthBarViewPresenter(INetworkHealthAdapter networkHealthAdapter, HealthBarView view)
        {
            _networkHealthAdapter = networkHealthAdapter;
            _view = view;
        }

        public void OnEnable()
        {
            _networkHealthAdapter.ServerHealthUpdated += UpdateHealthBar;

            _networkHealthAdapter.HealthUpdateRequest_Client();
        }

        public void OnDisable()
        {
            _networkHealthAdapter.ServerHealthUpdated -= UpdateHealthBar;
        }

        private void UpdateHealthBar(HealthData data)
        {
            _view.RenderHealth(data.CurrentHealth >= 0 ? data.CurrentHealth.ToString() : "0");
            _view.RenderFillAmount((float) data.CurrentHealth / data.MaxHealth);
        }
    }
}