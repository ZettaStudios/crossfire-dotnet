using crossfire_server.enums;
using crossfire_server.util;
using crossfire_server.util.log.Factories;

namespace crossfire_server.network.login.packet
{
    public class LoginResponsePacket : network.DataPacket
    {
        public new const short NetworkId = (short) LoginType.S2CValidAccount;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            
        }

        public override void Encode()
        {
            byte[] tmp = new byte[800];
            tmp[3] = 0; 
            tmp[4] = 25; 
            tmp[5] = 0;
            SetBuffer(tmp);
            buffer[0] = StartsWith;
            buffer[buffer.Length - 1] = EndsWith;
            LogFactory.GetLog("LoginResponsePacket").LogInfo($"\n{NetworkUtil.DumpPacket(buffer)}");
        }
    }
}