using Login.Enum;
using Shared.Network;

namespace Login.Network.packet
{
    public class LoginResponsePacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.S2CValidAccount;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            
        }

        // not finished
        public override void Encode()
        {
            buffer = new byte[809];
            buffer[0] = StartsWith;
            Write((ushort)buffer.Length - 9, 1);
            buffer[4] = 19;
            buffer[10] = 2;
            
            buffer[409] = 1;
            buffer[^1] = EndsWith;
        }
    }
}