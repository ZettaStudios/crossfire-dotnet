using Login.Enum;
using Shared.Network;

namespace Login.Network.packet
{
    public class LoginErrorResponsePacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.S2CDisplayError;

        public uint Identifier = 0;
        public ErrorsType Error = ErrorsType.UnknownError;
        
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
            if (Error == ErrorsType.PlayerAlreadyLoggedIn)
                tmp = Write(Identifier, 26, tmp);
            
            SetBuffer(tmp);

            buffer[1] = 160;
            buffer[2] = 31;
            
            buffer[3] = 0;
            buffer[4] = 1;
            buffer[5] = 0;
            
            buffer[0] = StartsWith;
            buffer[^9] = 1;
            buffer[^1] = EndsWith;
        }
    }
}