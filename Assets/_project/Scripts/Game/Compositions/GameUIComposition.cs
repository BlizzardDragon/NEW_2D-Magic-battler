using _project.Scripts.Core;
using _project.Scripts.Game.UI.GameOver;
using _project.Scripts.Game.UI.RestartButton;
using Cysharp.Threading.Tasks;
using VampireSquid.Common.Commands.Handlers;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Game.Compositions
{
    public class GameUIComposition : CompositionBase
    {
        private RestartButtonPresenter _restartButtonPresenter;

        public override async UniTask InstallBindings()
        {
            var gameOverView = GetLocal<GameOverView>();
            var restartButtonView = GetLocal<RestartButtonView>();
            var sceneLoader = GetGlobal<ISceneLoader>();

            var gameOverViewPresenter = new GameOverViewPresenter(gameOverView);
            _restartButtonPresenter = new RestartButtonPresenter(sceneLoader, restartButtonView);

            CommandHandler.Instance.AddListener(gameOverViewPresenter);
        }

        public override async UniTask Initialize()
        {
            _restartButtonPresenter.OnEnable();
        }

        public override void OnBeforeDisposed()
        {
            _restartButtonPresenter.OnDisable();
        }
    }
}