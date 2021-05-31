using Login.Enum;
using Shared.Network;

namespace Login.Network.packet
{
    public class SendServerListPacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.S2CGetServers;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            throw new System.NotImplementedException();
        }

        public override void Encode()
        {
            throw new System.NotImplementedException();
        }
    }
}