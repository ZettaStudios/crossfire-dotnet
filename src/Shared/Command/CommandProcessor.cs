using System;
using System.Threading;
using Shared.Util.Log.Factories;

namespace Shared.Command
{
    public class CommandProcessor
    {
        private readonly Server _server;
        private readonly Thread _thread;
        private readonly CommandMap _map;

        public CommandProcessor(Server server, CommandMap map)
        {
            _server = server;
            _map = map;
            _thread = new Thread(Processor);
        }

        // Thread methods / properties
        public void Start() => _thread.Start();
        public void Join() => _thread.Join();
        public bool IsAlive => _thread.IsAlive;
        
        private void Processor()
        {
            while (_server.IsAlive)
            {
                string[] args = Console.ReadLine()?.Split(" ");
                if (args != null && args.Length > 0)
                {
                    string label = args[0];
                    args = args[1..];
                    if (_map.Exists(label))
                    {
                        Command command = _map.GetCommand(label);
                        command.Execute(_server, args);
                    }
                    else
                    {
                        LogFactory.GetLog(_server.Name).LogWarning($"The command {label} doesn't exists, check name and try again.");
                    }
                }
            }
            _thread.Interrupt();
        }
    }
}