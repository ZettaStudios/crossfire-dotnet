using Game.Enum;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

namespace Game.Network.packet
{
    public class AnnouncementPacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.S2CAnnounce;
        
        public string Message = "";
        
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            throw new System.NotImplementedException();
        } 

        public override void Encode()
        {
            Memory.Position = 8;
            Write((ushort)2);
            Write(ushort.MaxValue);
            Write((ushort)1);
            Write((short)Message.Length);
            Memory.Position--;
            Write(Message);
            Write((byte)0);
            Write((byte)0);
            Memory.Position = 0;
            buffer = new byte[Memory.Length];
            Memory.Read(buffer, 0, buffer.Length);
            Close();
            buffer[0] = StartsWith;
            Write((ushort)buffer.Length - 9, 1);
            buffer[3] = 4; 
            buffer[4] = 8; 
            buffer[5] = 0;
            buffer[15] = 0;
            buffer[^1] = EndsWith;
        }
    }
}