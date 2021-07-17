using System.Text;
using Game.Enum;
using Shared.Model;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

namespace Game.Network.packet
{
    public class PlayerDataPacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.S2CPlayerData;
        public User User;
        public BattleStatistics Statistics;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
        }

        public override void Encode()
        {
            System.IO.MemoryStream memory = new System.IO.MemoryStream();
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(memory);
            memory.Position += 8;
            writer.Write((byte)0);
            writer.Write((uint)0);
            writer.Write(Encoding.Default.GetBytes(User.Id.ToString()));
            memory.Position += 21 - User.Id.ToString().Length;
            writer.Write((uint) 1);
            memory.Position += 1;
            writer.Write((uint)User.Honor);
            writer.Write(User.GamePoints);
            writer.Write(User.ZettaPoints);
            writer.Write(User.Experience); 
            memory.Position += 4;
            writer.Write(User.Rank);
            memory.Position += 4;
            var percent = 100 * User.Experience / User.ExperienceNeed;
            writer.Write((uint) percent);
            writer.Write(User.ExperienceNeed);
            writer.Write(Statistics.Wins);
            writer.Write(Statistics.Loses);
            writer.Write(Statistics.Kills);
            writer.Write(Statistics.Deaths);
            writer.Write(Statistics.Headshots);
            writer.Write(Statistics.Assists);
            writer.Write(Statistics.Desertion);
            writer.Write((uint)0);
            writer.Write((uint)15);
            writer.Write((uint)15);
            writer.Write(new byte[]{ 0xEC, 0xD8, 0xC0, 0x60 });
            memory.Position += 21;
            writer.Write(User.Id);
            memory.Position += 4;
            writer.Write(Encoding.Default.GetBytes(User.Nickname));
            memory.Position += 16 - Encoding.Default.GetBytes(User.Nickname).Length;
            writer.Write((ushort)2);
            writer.Write(Encoding.Default.GetBytes(User.Identifier));
            memory.Position += 20;
            writer.Write((short)16);
            writer.Write((short)6);
            writer.Write(Encoding.Default.GetBytes(User.Nickname));
            memory.Position += 20 - Encoding.Default.GetBytes(User.Nickname).Length;
            writer.Write(0);
            int length = (int) memory.Position;
            memory.Position = 0;
            buffer = new byte[3303]; // 3161 pharaoh
            memory.Read(buffer, 0, length);
            writer.Close();
            memory.Close();
            buffer[0] = StartsWith;
            Write((ushort)buffer.Length - 9, 1);
            buffer[3] = 1; 
            buffer[4] = 1;
            buffer[^1] = EndsWith;
            LogFactory.GetLog("PlayerData:Encoded").LogInfo($"\n{NetworkUtil.DumpPacket(buffer)}");
        }
    }
}