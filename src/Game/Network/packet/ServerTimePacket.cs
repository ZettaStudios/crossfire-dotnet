using System;
using Game.Enum;
using Shared.Model;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

namespace Game.Network.packet
{
    public class ServerTimePacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.C2SServerTime;
        public User User;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            LogFactory.GetLog("ServerTimePacket:Decoded").LogInfo($"\n{NetworkUtil.DumpPacket(buffer)}");
        }

        public override void Encode()
        {
            buffer = new byte[209];
            Write(45138, 14);
            Write("pY20", 16);
            Write(44431, 22);
            Write("lY", 24);
            Write(DateTime.Now.ToString("yyyymmddhhmmss"), 26);
            Write(User.Rank, 94);
            Write(User.Kills, 98);
            Write(User.Deaths, 102);
            Write(User.Identifier, 106);
            Close();
            buffer[3] = 1; 
            buffer[4] = 6; 
            buffer[5] = 0;
            LogFactory.GetLog("ServerTimePacket:Encoded").LogInfo($"\n{NetworkUtil.DumpPacket(buffer)}");
        }
    }
}