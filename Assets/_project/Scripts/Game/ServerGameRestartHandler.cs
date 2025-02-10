using _project.Scripts.Core;
using Cysharp.Threading.Tasks;

namespace _project.Scripts.Game
{
    public class ServerGameRestartHandler
    {
        private readonly INetworkGameRestartAdapter _networkGameRestartAdapter;
        private readonly ISceneLoader _sceneLoader;

        public ServerGameRestartHandler(INetworkGameRestartAdapter networkGameRestartAdapter, ISceneLoader sceneLoader)
        {
            _networkGameRestartAdapter = networkGameRestartAdapter;
            _sceneLoader = sceneLoader;
        }

        public void OnEnable()
        {
            _networkGameRestartAdapter.RestartRequested += Restart;
        }

        public void OnDisable()
        {
            _networkGameRestartAdapter.RestartRequested -= Restart;
        }

        private void Restart()
        {
            if (NetworkManager.Instance.IsHost) return;

            _sceneLoader.RestartAsync().Forget();
        }
    }
}