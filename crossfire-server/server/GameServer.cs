namespace crossfire_server.server
{
    public class GameServer : Server
    {
        public GameServer(string[] args) : base(args)
        {
            name = "Game Server";
            port = 13009;
        }
    }
}