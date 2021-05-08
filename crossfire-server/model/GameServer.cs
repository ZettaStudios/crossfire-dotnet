using crossfire_server.enums;

namespace crossfire_server.model
{
    public class GameServer
    {
        public string Name;

        public int Port; // (ushort)

        public uint MaxPlayers;

        public byte[] AddressBytes;

        public string Address;

        public ushort NoLimit;

        public ushort MinRank;

        public ushort MaxRank;

        public ServerType serverType;
    }
}