using Game.Enum;
using Shared.Network;

namespace Game.Network.packet
{
    public class AuthToChannelServerPacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.C2SAuthToChannelServer;
        public string Identifier;
        public string Username;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            Username = ToString(16, 13);
            Identifier = ToString(313, 31);
        }

        public override void Encode()
        {
            buffer = new byte[13];
            buffer[0] = StartsWith;
            Write((ushort)buffer.Length - 9, 1);
            buffer[3] = 0x1; 
            buffer[4] = 0x9; 
            buffer[5] = 0x0;
            buffer[8] = 0x1;
            buffer[^1] = EndsWith;
        }
    }
}