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
            
            System.IO.MemoryStream memory = new System.IO.MemoryStream();
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(memory);
            memory.Position = 8;
            writer.Write((ushort)2);
            writer.Write(ushort.MaxValue);
            writer.Write((ushort)1);
            writer.Write((short)Message.Length);
            memory.Position--;
            writer.Write(Message);
            writer.Write((byte)0);
            writer.Write((byte)0);
            memory.Position = 0;
            buffer = new byte[memory.Length];
            memory.Read(buffer, 0, buffer.Length);
            writer.Close();
            memory.Close();
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