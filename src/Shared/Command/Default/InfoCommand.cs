using Shared.Util.Log.Factories;

namespace Shared.Command.Default
{
    public class InfoCommand : Command
    {
        public InfoCommand ()
        {
            Name = "info";
        }

        public override void Execute(Server server, string[] args)
        {
            LogFactory.GetLog(server.Name).LogInfo(server.GetServerInfo());
        }
    }
}