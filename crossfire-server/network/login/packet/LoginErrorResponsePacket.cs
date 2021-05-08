using crossfire_server.enums;
using crossfire_server.util;
using crossfire_server.util.log.Factories;

namespace crossfire_server.network.login.packet
{
    public class LoginErrorResponsePacket : network.DataPacket
    {
        public new const short NetworkId = (short) LoginType.S2CDisplayError;

        public uint Identifier = 0;
        public LoginErrorsType Error = LoginErrorsType.UnknownError;
        
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            
        }
        public override void Encode()
        {
            byte[] tmp = new byte[8096];
            tmp = Write((byte)Error, 0, tmp);
            if (Error == LoginErrorsType.PlayerAlreadyLoggedIn)
                tmp = Write(Identifier, 26, tmp);
            
            SetBuffer(tmp);

            buffer[1] = 160;
            buffer[2] = 31;
            
            buffer[3] = 0;
            buffer[4] = 1;
            buffer[5] = 0;
            
            buffer[0] = StartsWith;
            buffer[buffer.Length - 9] = 1;
            buffer[buffer.Length - 1] = EndsWith;
        }
    }
}