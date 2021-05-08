using crossfire_server.enums;

namespace crossfire_server.network.login.packet
{
    public class SendServerListPacket : network.DataPacket
    {
        public new const short NetworkId = (short) LoginType.S2CGetServers;
        public override short Pid()
        {
            throw new System.NotImplementedException();
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