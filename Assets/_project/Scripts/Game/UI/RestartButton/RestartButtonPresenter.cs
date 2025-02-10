using _project.Scripts.Core;
using Cysharp.Threading.Tasks;

namespace _project.Scripts.Game.UI.RestartButton
{
    public class RestartButtonPresenter
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly INetworkGameRestartAdapter _networkGameRestartAdapter;
        private readonly RestartButtonView _view;

        public RestartButtonPresenter(
            ISceneLoader sceneLoader,
            INetworkGameRestartAdapter networkGameRestartAdapter,
            RestartButtonView view)
        {
            _sceneLoader = sceneLoader;
            _networkGameRestartAdapter = networkGameRestartAdapter;
            _view = view;
        }

        public void OnEnable()
        {
            _view.ButtonClicked += OnButtonClicked;
        }

        public void OnDisable()
        {
            _view.ButtonClicked -= OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            _networkGameRestartAdapter.RestartRequest_Client();
            _sceneLoader.RestartAsync().Forget();
        }
    }
}