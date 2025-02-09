using _project.Scripts.Entities.Health;

namespace _project.Scripts.Entities.Unit.Network.Health
{
    public class ServerHealthSyncHandler
    {
        private readonly IHealth _health;
        private readonly INetworkHealthAdapter _networkHealthAdapter;

        public ServerHealthSyncHandler(IHealth health, INetworkHealthAdapter networkHealthAdapter)
        {
            _health = health;
            _networkHealthAdapter = networkHealthAdapter;
        }

        public void OnEnable()
        {
            _health.HealthChanged += OnHealthChanged;
            _networkHealthAdapter.ClientHealthUpdateRequested += OnHealthChanged;
        }

        public void OnDisable()
        {
            _health.HealthChanged -= OnHealthChanged;
            _networkHealthAdapter.ClientHealthUpdateRequested -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            var data = new HealthData(_health.CurrentHealth, _health.MaxHealth);
            _networkHealthAdapter.HealthChanged_Serve(data);
        }
    }
}