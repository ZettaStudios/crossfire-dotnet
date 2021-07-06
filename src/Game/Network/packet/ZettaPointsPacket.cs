using Game.Enum;
using Shared.Model;
using Shared.Network;

namespace Game.Network.packet
{
    public class ZettaPointsPacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.C2SGetZp;
        public User User;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
        }

        public override void Encode()
        {
            buffer = new byte[11];
            buffer[0] = StartsWith;
            Write((ushort)buffer.Length - 9, 1);
            buffer[3] = 1; 
            buffer[4] = 0x81; 
            buffer[5] = 0;
            Write(User.ZettaPoints, 6);
            buffer[^1] = EndsWith;
        }
    }
}