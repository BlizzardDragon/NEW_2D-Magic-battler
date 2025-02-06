using _project.Scripts.Game.UI;
using Cysharp.Threading.Tasks;
using VampireSquid.Common.Commands.Handlers;
using VampireSquid.Common.CompositeRoot;

namespace _project.Scripts.Game.Compositions
{
    public class GameUIComposition : CompositionBase
    {
        private GameOverViewPresenter _gameOverViewPresenter;

        public override async UniTask InstallBindings()
        {
            var gameOverView = GetLocal<GameOverView>();

            _gameOverViewPresenter = new GameOverViewPresenter(gameOverView);

            CommandHandler.Instance.AddListener(_gameOverViewPresenter);
        }
    }
}