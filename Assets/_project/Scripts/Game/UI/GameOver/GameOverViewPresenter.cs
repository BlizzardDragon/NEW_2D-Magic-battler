using _project.Scripts.Game.Commands;
using VampireSquid.Common.Commands;

namespace _project.Scripts.Game.UI.GameOver
{
    public class GameOverViewPresenter : ICommandListener<WinGameCommand>, ICommandListener<LoseGameCommand>
    {
        private const string YOU_WIN = "You win!";
        private const string YOU_LOSE = "You lose!";

        private readonly GameOverView _view;

        public GameOverViewPresenter(GameOverView view)
        {
            _view = view;
        }

        public void ReactCommand(WinGameCommand command)
        {
            _view.RenderText(YOU_WIN);
        }

        public void ReactCommand(LoseGameCommand command)
        {
            _view.RenderText(YOU_LOSE);
        }
    }
}