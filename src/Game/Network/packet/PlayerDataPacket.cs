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
            Memory.Position += 8;
            Write((byte)0);
            Write((uint)0);
            Write(Encoding.Default.GetBytes(User.Id.ToString()));
            Memory.Position += 21 - User.Id.ToString().Length;
            Write((uint) 1);
            Memory.Position += 1;
            Write((uint)User.Honor);
            Write(User.GamePoints);
            Write(User.ZettaPoints);
            Write(User.Experience); 
            Memory.Position += 4;
            Write(User.Rank);
            Memory.Position += 4;
            var percent = 100 * User.Experience / User.ExperienceNeed;
            Write((uint) percent);
            Write(User.ExperienceNeed);
            Write(Statistics.Wins);
            Write(Statistics.Loses);
            Write(Statistics.Kills);
            Write(Statistics.Deaths);
            Write(Statistics.Headshots);
            Write(Statistics.Assists);
            Write(Statistics.Desertion);
            Write((uint)0);
            Write((uint)15);
            Write((uint)15);
            Write(new byte[]{ 0xEC, 0xD8, 0xC0, 0x60 });
            Memory.Position += 21;
            Write(User.Id);
            Memory.Position += 4;
            Write(Encoding.Default.GetBytes(User.Nickname));
            Memory.Position += 16 - Encoding.Default.GetBytes(User.Nickname).Length;
            Write((ushort)2);
            Write(Encoding.Default.GetBytes(User.Identifier));
            Memory.Position += 20;
            Write((short)16);
            Write((short)6);
            Write(Encoding.Default.GetBytes(User.Nickname));
            Memory.Position += 20 - Encoding.Default.GetBytes(User.Nickname).Length;
            Write(0);
            int length = (int) Memory.Position;
            Memory.Position = 0;
            buffer = new byte[3303]; // 3161 pharaoh
            Memory.Read(buffer, 0, length);
            Close();
            buffer[3] = 1; 
            buffer[4] = 1;
            LogFactory.GetLog("PlayerData:Encoded").LogInfo($"\n{NetworkUtil.DumpPacket(buffer)}");
        }
    }
}