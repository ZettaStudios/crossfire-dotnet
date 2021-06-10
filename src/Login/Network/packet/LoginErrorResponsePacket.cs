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
            buffer = new byte[8213];
            buffer = Write((byte)Error, 8, buffer);
            if (Error == ErrorsType.PlayerAlreadyLoggedIn)
                buffer = Write(Identifier, 35, buffer);
            
            Write((ushort)buffer.Length - 9, 1);
            
            buffer[3] = 0;
            buffer[4] = 1;
            buffer[5] = 0;
            
            buffer[0] = StartsWith;
            buffer[^20] = 1;
            buffer[^18] = 0x10;
            buffer[^17] = 1;
            buffer[^1] = EndsWith;
        }
    }
}