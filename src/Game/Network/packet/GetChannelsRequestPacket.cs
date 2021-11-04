using Game.Enum;
using Shared;
using Shared.Network;

namespace Game.Network.packet
{
    public class GetChannelsRequestPacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.C2SGetChannels;
        public Server Server;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
        }

        public override void Encode()
        {
            GameServer server = (GameServer) Server;
            ushort channelCapacity = (ushort) (server.MaxConnections / server.ChannelsCount);
            for (ushort i = 1; i <= server.ChannelsCount; i++)
            {
                Write((ushort)(i - 1));
                Write(channelCapacity);
                Write((ushort)10); //Current Number of Players on the Channel
                Memory.Position += 14;
            }
            Memory.Position = 0;
            buffer = new byte[Memory.Length];
            Memory.Read(buffer, 0, buffer.Length);
            Close();
        }
    }
}