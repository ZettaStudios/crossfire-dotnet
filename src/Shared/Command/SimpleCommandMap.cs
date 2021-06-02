using Shared.Command.Default;

namespace Shared.Command
{
    public class SimpleCommandMap : CommandMap
    {
        public SimpleCommandMap()
        {
            RegisterCommands();
        }
        protected sealed override void RegisterCommands()
        {
            Register<StopCommand>("stop");
            Register<InfoCommand>("info");
        }
    }
}