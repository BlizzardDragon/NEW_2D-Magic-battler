using _project.Scripts.Entities.Health;
using _project.Scripts.Game;
using _project.Scripts.Game.Commands;
using VampireSquid.Common.Commands.Handlers;

namespace _project.Scripts.Entities.Unit
{
    public class UnitDeathPresenter
    {
        private readonly IHealth _health;
        private readonly ITurnPipelineRunner _turnPipelineRunner;

        public UnitDeathPresenter(IHealth health, ITurnPipelineRunner turnPipelineRunner)
        {
            _health = health;
            _turnPipelineRunner = turnPipelineRunner;
        }

        public void OnEnable()
        {
            _health.Died += OnDied;
        }

        public void OnDisable()
        {
            _health.Died -= OnDied;
        }

        private void OnDied()
        {
            _turnPipelineRunner.Stop();
            CommandHandler.Instance.SendCommand(new FinishGameCommand());
        }
    }
}