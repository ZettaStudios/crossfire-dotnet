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

        // Not Finished
        public override void Encode()
        {
            byte[] tmp = new byte[8096];
            buffer = Write((byte)ErrorsType.NoError, 0, tmp);
            buffer[3] = 0;
            buffer[4] = 1;
            buffer[5] = 0;
            buffer[0] = StartsWith;
            buffer[^1] = EndsWith;
        }
    }
}