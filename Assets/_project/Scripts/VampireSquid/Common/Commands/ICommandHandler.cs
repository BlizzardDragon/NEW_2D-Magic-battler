namespace VampireSquid.Common.Commands
{
    public interface ICommandHandler
    {
        void AddListener(object             commandsListener);
        void RemoveListener(object          commandsListener);
        void SendCommand<TCommand>(TCommand command = default) where TCommand : struct, ICommand;
        void CleanUp();
    }
}
