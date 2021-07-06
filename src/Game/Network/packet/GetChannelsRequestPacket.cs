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
            GameServer _server = (GameServer) Server;
            System.IO.MemoryStream memory = new System.IO.MemoryStream();
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(memory);
            ushort channelCapacity = (ushort) (_server.MaxConnections / _server.ChannelsCount);
            for (ushort i = 1; i <= _server.ChannelsCount; i++)
            {
                writer.Write((ushort)(i - 1));
                writer.Write(channelCapacity);
                writer.Write((ushort)10);//Current Number of Players on the Channel
                memory.Position += 14;
            }
            memory.Position = 0;
            buffer = new byte[memory.Length];
            memory.Read(buffer, 0, buffer.Length);
            writer.Close();
            memory.Close();
            buffer[0] = StartsWith;
            Write((ushort)buffer.Length - 9, 1);
            buffer[^1] = EndsWith;
        }
    }
}