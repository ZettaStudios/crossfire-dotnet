using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Shared.Command
{
    public abstract class CommandMap
    {
        protected ConcurrentDictionary<string, Type> CommandPool = new ConcurrentDictionary<string, Type>();
        
        public bool Exists(string key)
        {
            return CommandPool.ContainsKey(key);
        }
        
        public Command GetCommand(string id) {
            if (CommandPool.ContainsKey(id))
            {
                return (Command) Activator.CreateInstance(CommandPool[id]);
            }

            return null;
        }
        
        public Dictionary<string, Command> GetCommands()
        {
            Dictionary<string, Command> commands = new Dictionary<string, Command>();
            foreach (var keyValuePair in CommandPool)
            {
                commands.Add(keyValuePair.Key, (Command) Activator.CreateInstance(keyValuePair.Value));
            }
            return commands;
        }
        
        public void Register<TCommand>(string id) where TCommand : Command
        {
            
            Type dType = typeof(TCommand);
            if (!CommandPool.ContainsKey(id))
            {
                CommandPool.TryAdd(id, dType);
            }
        }

        protected abstract void RegisterCommands();
    }
}