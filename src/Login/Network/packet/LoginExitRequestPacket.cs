using Login.Enum;
using Shared.Network;

namespace Login.Network.packet
{
    public class LoginExitRequestPacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.C2SExit;

        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            
        }

        public override void Encode()
        {
            buffer[4] = 12;
        }
    }
}