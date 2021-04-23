using crossfire_server.enums;
using crossfire_server.util;

namespace crossfire_server.network.login.packet
{
    public class LoginErrorResponsePacket : network.DataPacket
    {
        public const short NetworkId = (short) LoginType.S2CDisplayError;

        public uint Identifier = 0;
        public LoginErrorsType Error = LoginErrorsType.UnknownError;
        
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            
        }
        // IN        x  y  z       ER 
        // f1 7b 1e 00 01 00 00 00 04 00
        // F1 7B 1E 00 01 00 00 00 04 00
        // F1 7B 1E 00 01 00 00 00 02 00
        
        // f1 a0 1f 00 01 00 00 00 04 00
        public override void Encode()
        {
            byte[] tmp = new byte[1445];
            tmp = Write((byte)Error, 2, tmp);
            if (Error == LoginErrorsType.PlayerAlreadyLoggedIn)
                tmp = Write(Identifier, 26, tmp);
            
            SetBuffer(tmp);

            buffer[1] = 160;
            buffer[2] = 31;
            
            buffer[3] = 0; // x
            buffer[4] = 1; // y
            buffer[5] = 0; // z
            
            buffer[0] = StartsWith;
            buffer[buffer.Length - 1] = 0;
            Debug();
        }

        public void Debug()
        {
            Console.Log($"[Login Server] [{LoginType.S2CDisplayError.ToString()}]");
            Console.Log($"{NetworkUtil.DumpPacket(buffer)}");
            Console.Log("----- PACKET END -----");
        }
    }
}