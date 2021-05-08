using crossfire_server.enums;
using crossfire_server.model;

namespace crossfire_server.server
{
    public class GameServer : Server
    {
        protected GameServerInfo _info;
        
        public GameServer(string[] args) : base(args)
        {
            name = "Game Server";
            type = ServerType.Game;
            port = 13009;
        }

        public GameServerInfo Info
        {
            get => _info;
            set => _info = value;
        }
    }
}