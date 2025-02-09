using System;

namespace _project.Scripts.Entities.Unit.Network.Health
{
    public interface INetworkHealthAdapter
    {
        event Action ClientHealthUpdateRequested;
        event Action<HealthData> ServerHealthUpdated;

        void HealthUpdateRequest_Client();
        void HealthChanged_Serve(HealthData data);
    }

    public class NetworkHealthAdapter : INetworkHealthAdapter
    {
        public event Action ClientHealthUpdateRequested;
        public event Action<HealthData> ServerHealthUpdated;

        public void HealthUpdateRequest_Client()
        {
            HealthUpdateRequest_Server();
        }

        private void HealthUpdateRequest_Server()
        {
            ClientHealthUpdateRequested?.Invoke();
        }

        public void HealthChanged_Serve(HealthData data)
        {
            HealthChanged_Client(data);
        }

        private void HealthChanged_Client(HealthData data)
        {
            ServerHealthUpdated.Invoke(data);
        }
    }
}