using Login.Enum;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

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

        public override void Encode()
        {
            byte[] tmp = new byte[790];
            tmp[1] = 32;
            tmp[2] = 3;
            tmp[4] = 25;
            tmp[8] = 2;
            SetBuffer(tmp);
            LogFactory.GetLog("LoginResponsePacket").LogInfo($"\n{NetworkUtil.DumpPacket(buffer)}");
        }
    }
}