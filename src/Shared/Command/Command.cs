namespace Shared.Command
{
    public abstract class Command
    {
        public string Name { get; set; }

        public virtual void Execute(Server server)
        {
            this.Execute(server, new string[0]);
        }

        public virtual void Execute(Server server, string[] args)
        {

        }
    }
}