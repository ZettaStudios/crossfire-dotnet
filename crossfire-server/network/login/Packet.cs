using crossfire_server.enums;
using crossfire_server.util;

namespace crossfire_server.network.login
{
    public class DataPacket
    {
        private byte[] buffer;
        public LoginType type = LoginType.Unknown;
        
        public DataPacket(LoginType type, byte[] data, int optionalLength = 0)
        {
            buffer = new byte[data.Length + 7];
            
            //buffer[0] = StartsWith;
            //buffer[buffer.Length - 1] = EndsWith;
            
            for (int i = 6; i < (data.Length + 6); i++)
            {
                buffer[i] = data[i - 6];
            }
            
            //Write(optionalLength == 0 ? data.Length : optionalLength, 1);
            
            this.type = type;
            switch (this.type)
            {
                case LoginType.S2CDisplayError:            buffer[3] = 0; buffer[4] = 1; buffer[5] = 0; break;
                case LoginType.S2CGetServers:              buffer[3] = 0; buffer[4] = 1; buffer[5] = 0; break;
                case LoginType.S2CGoBackForServers:        buffer[3] = 0; buffer[4] = 3; buffer[5] = 0; break;
                case LoginType.S2CTryEnter:                buffer[3] = 0; buffer[4] = 7; buffer[5] = 2; break;
                case LoginType.S2CCreateAccount:           buffer[3] = 0; buffer[4] = 9; buffer[5] = 0; break;
                case LoginType.S2CCheckNameExist:      buffer[3] = 0; buffer[4] = 11; buffer[5] = 0; break;
                case LoginType.S2CPlayerHasBeenLoggedOut:  buffer[3] = 0; buffer[4] = 13; buffer[5] = 0; break;
                case LoginType.S2CLoginToGameServerStep1: buffer[3] = 0; buffer[4] = 16; buffer[5] = 0; break;
                case LoginType.S2CLoginToGameServerStep2: buffer[3] = 0; buffer[4] = 18; buffer[5] = 0; break;
                case LoginType.S2CExitGameInfo:            buffer[3] = 0; buffer[4] = 22; buffer[5] = 0; break;
                case LoginType.S2CValidAccount:            buffer[3] = 0; buffer[4] = 25; buffer[5] = 0; break;
            }
        }
    }
}