using Login.Enum;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

namespace Login.Network.packet
{
    public class CheckNameExistencePacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.C2SCheckNameExists;
        public string Nickname = "";
        public bool Exists;
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
        }

        public override void Encode()
        {
            buffer = new byte[13];
            buffer[0] = StartsWith;
            Write((ushort)buffer.Length - 9, 1);
            buffer[4] = 11; // ?? - 9 | ?? - 11
            buffer[8] = (byte) (Exists ? 2 : 0);
            buffer[^1] = EndsWith;
        }
    }
}