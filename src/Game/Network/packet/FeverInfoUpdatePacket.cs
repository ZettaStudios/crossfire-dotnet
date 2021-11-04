using System;
using Game.Enum;
using Game.Session;
using Shared.Model;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

namespace Game.Network.packet
{
    public class FeverInfoUpdatePacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.C2SFeverInfoUpdate;
        public GameSession Session;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            LogFactory.GetLog("FeverInfoUpdatePacket:Decoded").LogInfo($"\n{NetworkUtil.DumpPacket(buffer)}");
        }

        public override void Encode()
        {
            FeverData data = Session.Fever;
            if (!data.Activated)
            {
                Write((ushort)0);
                Write((ushort)0);
                Write((uint)data.Percent);
                Write((ushort)data.Progress);//May be the number of progresses passed
                Write((ushort)100);//Max percent
            }
            else
            {
                Write((ushort)3);
                Write((ushort)0);
                Write((uint)((data.ActivatedAt.AddMinutes(data.Duration) - DateTime.Now).TotalMinutes));//Remaining Minutes
                Write((ushort)data.Progress);//May be the number of progresses passed
                Write((ushort)100);//Max percent
            }
            Memory.Position = 0;
            buffer = new byte[Memory.Length];
            Memory.Read(buffer, 0, buffer.Length);
            Close();
            buffer[3] = 1; 
            buffer[4] = 169; 
            buffer[5] = 2;
            LogFactory.GetLog("FeverInfoUpdatePacket:Encoded").LogInfo($"\n{NetworkUtil.DumpPacket(buffer)}");
        }
    }
}