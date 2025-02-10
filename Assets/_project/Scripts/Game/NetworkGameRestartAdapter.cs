using System;

namespace _project.Scripts.Game
{
    public interface INetworkGameRestartAdapter
    {
        event Action RestartRequested;
        
        void RestartRequest_Client();
    }
    
    public class NetworkGameRestartAdapter : INetworkGameRestartAdapter
    {
        public event Action RestartRequested;
        
        public void RestartRequest_Client()
        {
            RestartRequest_Server();
        }
        
        private void RestartRequest_Server()
        {
            RestartRequested?.Invoke();
        }
    }
}