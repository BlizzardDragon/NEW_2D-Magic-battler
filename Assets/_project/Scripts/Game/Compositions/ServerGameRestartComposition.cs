using _project.Scripts.Core;
using Cysharp.Threading.Tasks;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Game.Compositions
{
    public class ServerGameRestartComposition : CompositionBase
    {
        private ServerGameRestartHandler _serverGameRestartHandler;

        public override async UniTask InstallBindings()
        {
            var sceneLoader = GetGlobal<ISceneLoader>();

            var networkGameRestartAdapter = new NetworkGameRestartAdapter();

            _serverGameRestartHandler = new ServerGameRestartHandler(networkGameRestartAdapter, sceneLoader);

            BindAsLocal<INetworkGameRestartAdapter>(networkGameRestartAdapter);
        }

        public override async UniTask Initialize()
        {
            _serverGameRestartHandler.OnEnable();
        }

        public override void OnBeforeDisposed()
        {
            _serverGameRestartHandler.OnDisable();
        }
    }
}