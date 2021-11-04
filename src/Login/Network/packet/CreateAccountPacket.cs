using Login.Enum;
using Shared.Network;

namespace Login.Network.packet
{
    public class CreateAccountPacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.C2SCreateAccount;
        public string Nickname = "";
        public bool Confirmed = false;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            int i = 8;
            while (buffer[i] != 0)
            {
                i++;
            }
            i -= 8;
            Nickname = ToString(8, i);
            Confirmed = buffer[4] != 9;
        }

        public override void Encode()
        {
            buffer = new byte[13];
            Write((ushort)buffer.Length - 9, 1);
            buffer[0] = StartsWith;
            buffer[4] = 9;
            buffer[^1] = EndsWith;
        }
    }
}