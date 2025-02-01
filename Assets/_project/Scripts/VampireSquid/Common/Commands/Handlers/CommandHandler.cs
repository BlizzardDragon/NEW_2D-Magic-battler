using System;
using System.Collections.Generic;

namespace VampireSquid.Common.Commands.Handlers
{
    public class CommandHandler : ICommandHandler
    {
        private readonly Dictionary<Type, List<object>> listenersMap = new(256);

        private static CommandHandler _instance;
        public static  CommandHandler Instance => _instance ??= new CommandHandler();

        public void AddListener(object commandsListener)
        {
            var type = commandsListener.GetType();

            foreach (var implementedInterface in type.GetInterfaces())
            {
                if (implementedInterface.IsGenericType
                    && implementedInterface.GetGenericTypeDefinition() == typeof(ICommandListener<>))
                {
                    var commandType = implementedInterface.GetGenericArguments()[0];

                    if (!listenersMap.TryGetValue(commandType, out var listeners))
                    {
                        listeners = new List<object>();
                        listenersMap[commandType] = listeners;
                    }

                    listeners.Add(commandsListener);
                }
            }
        }
        
        public void RemoveListener(object commandsListener)
        {
            var type = commandsListener.GetType();

            foreach (var implementedInterface in type.GetInterfaces())
            {
                if (implementedInterface.IsGenericType
                    && implementedInterface.GetGenericTypeDefinition() == typeof(ICommandListener<>))
                {
                    var commandType = implementedInterface.GetGenericArguments()[0];

                    if (listenersMap.TryGetValue(commandType, out var listeners))
                    {
                        listeners.Remove(commandsListener);

                        if (listeners.Count == 0)
                        {
                            listenersMap.Remove(commandType);
                        }
                    }
                }
            }
        }

        public void SendCommand<TCommand>(TCommand command = default) where TCommand : struct, ICommand
        {
            var commandType = typeof(TCommand);
            
            if (listenersMap.TryGetValue(commandType, out var listeners))
            {
                foreach (var listener in listeners)
                {
                    if (listener is ICommandListener<TCommand> commandListener)
                        commandListener.ReactCommand(command);
                }
            }
        }

        public void CleanUp() => listenersMap.Clear();

        public CommandHandler NewInstance() => _instance = new();
    }
}
