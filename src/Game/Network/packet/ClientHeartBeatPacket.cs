using System;
using Game.Enum;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

namespace Game.Network.packet
{
    public class ClientHeartBeatPacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.C2SHeartBeat;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
           // LogFactory.GetLog("HeartBeat:Decoded").LogInfo($"\n{NetworkUtil.DumpPacket(buffer)}");
        }

        public override void Encode()
        {
            byte[] tmp = BitConverter.GetBytes(BitConverter.ToUInt32(buffer, 6));
            buffer = new byte[13];
            buffer[0] = StartsWith;
            Write((ushort)buffer.Length - 9, 1);
            buffer[3] = 1; 
            buffer[4] = 0xAC; 
            buffer[5] = 0;
            buffer[^1] = EndsWith;
            Write(tmp, 6);
        }
    }
}