using crossfire_server.enums;

namespace crossfire_server.model
{
    public class GameServerInfo
    {
        public string Name;

        public int Port;

        public uint MaxPlayers;

        public byte[] IpBytes;

        public string Ip;

        public ushort NoLimit;

        public ushort MinRank;

        public ushort MaxRank;

        public GameServerType Type;
    }
}