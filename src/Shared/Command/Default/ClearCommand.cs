using System;
using Shared.Util.Log.Factories;

namespace Shared.Command.Default
{
    public class ClearCommand : Command
    {
        public ClearCommand ()
        {
            Name = "clear";
        }

        public override void Execute(Server server, string[] args)
        {
            Console.Clear();
            LogFactory.GetLog(server.Name).LogInfo("Console has been cleared.");
        }
    }
}