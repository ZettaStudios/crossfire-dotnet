using crossfire_server.enums;

namespace crossfire_server.network.login.packet
{
    public class LoginResponsePacket : network.DataPacket
    {
        public const short NetworkId = (short) LoginType.S2CValidAccount;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            
        }

        public override void Encode()
        {
            
        }
    }
}