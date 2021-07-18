using System;
using Login.Enum;
using Shared.Model;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

namespace Login.Network.packet
{
    public class LoginToGameServerRequestStep1Packet : DataPacket
    {
        public new const short NetworkId = (short) PacketType.C2SLoginToGameServerStep1;
        public int ServerId = -1;
        public User User;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            ServerId = buffer[8];
        }

        public override void Encode()
        {
            buffer = new byte[207];
            buffer[0] = StartsWith;
            Write((ushort)buffer.Length - 9, 1);
            buffer[3] = 0; 
            buffer[4] = 16; 
            buffer[5] = 0;
            Write(45138, 14, buffer);
            Write("pY20", 16, buffer);
            Write(44431, 22, buffer);
            Write("lY", 24, buffer);
            Write(DateTime.Now.ToString("yyyymmddhhmmss"), 26, buffer);
            Write(User.Rank, 94, buffer);
            Write(User.Kills, 98, buffer);
            Write(User.Deaths, 102, buffer);
            Write(User.Identifier, 106, buffer);
            buffer[^1] = EndsWith;
            
            // LogFactory.GetLog("LoginToGameServerRequest").LogWarning($"\n{NetworkUtil.DumpPacket(buffer)}");
        }
    }
}