using _project.Scripts.Entities.Health;
using _project.Scripts.Game.Commands;
using VampireSquid.Common.Commands.Handlers;

namespace _project.Scripts.Entities.Unit.Enemy
{
    public class EnemyDeathPresenter
    {
        private readonly IHealth _health;

        public EnemyDeathPresenter(IHealth health)
        {
            _health = health;
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
            CommandHandler.Instance.SendCommand(new WinGameCommand());
        }
    }
}