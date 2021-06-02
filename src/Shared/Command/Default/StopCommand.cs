namespace Shared.Command.Default
{
    public class StopCommand : Command
    {
        public StopCommand ()
        {
            Name = "stop";
        }

        public override void Execute(Server server, string[] args)
        {
            server.Stop();
        }
    }
}