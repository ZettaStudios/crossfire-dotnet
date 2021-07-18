using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Login.Enum;
using Shared.Model;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

namespace Login.Network.packet
{
    public class SendServerListPacket : DataPacket
    {
        public new const short NetworkId = (short) PacketType.S2CGetServers;
        public List<GameServer> Servers;
        public User User;
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            throw new NotImplementedException();
        }

        public override void Encode()
        {
            buffer = new byte[8213];
            #region Body
            MemoryStream memory = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memory);
            memory.Position += 8; // 12
            writer.Write(User.IsFirstTimeJoined() ? 6 : 0);
            if (User.IsFirstTimeJoined())
            {
                memory.Position += 13;
            }
            else
            {
                writer.Write(Encoding.ASCII.GetBytes(User.Nickname));
                writer.Write(new byte[13 - User.Nickname.Length]);
            }
            writer.Write(User.Id); // USER ID
            memory.Position -= 1;
            writer.Write(0x01); //(ushort)
            memory.Position += 3;
            writer.Write(User.Rank); // RANK
            
            writer.Write(User.Kills); // TOTAL KILLS
            writer.Write(User.Deaths); // TOTAL DEATHS
            writer.Write(0u);
            memory.Position += 2;
            writer.Write(NetworkUtil.StringToBytes("F0 3F")); // USER FOOTER
            if (Servers != null)
            {
                writer.Write((short)Servers.Count);
                for (int i = 0; i < Servers.Count; i++)
                {
                    GameServer server = Servers[i];
                    writer.Write((ushort) server.Type);
                    writer.Write(server.NoLimit);
                    writer.Write(server.MinRank);
                    writer.Write(server.MaxRank);
                    memory.Position += 16;
                    writer.Write((ushort)(i + 1)); // 22867
                    writer.Write(Encoding.ASCII.GetBytes(server.Name));
                    writer.Write(new byte[31 - server.Name.Length]);
                    writer.Write(server.Port);
                    memory.Position += 2; // 64 = 100
                    writer.Write(IPAddress.Parse(server.Address).GetAddressBytes());
                    writer.Write(100u);
                    writer.Write((ulong)(server.PlayersOnline / (double)server.Limit * 100d));
                    writer.Write(IPAddress.Parse(server.Address).GetAddressBytes());
                }
            }
            memory.Position = 0;
            memory.Read(buffer, 0, buffer.Length);
            writer.Flush();
            memory.Flush();
            writer.Close();
            memory.Close();
            #endregion

            #region Header
            buffer[0] = StartsWith;
            Write((ushort)buffer.Length - 9, 1);
            buffer[4] = 0x01;
            #endregion
            
            Footer();
            buffer[^1] = EndsWith;
            LogFactory.GetLog("SendServerListPacket").LogWarning($"\n{NetworkUtil.DumpPacket(buffer)}");
        }
        
        private void Footer()
        {
            buffer[^53] = 0x24;
            buffer[^52] = 0x48;
            buffer[^49] = 0x24;
            buffer[^48] = 0x6C;
            buffer[^47] = 0x6C;
            buffer[^46] = 0x6C;
            buffer[^45] = 0x72;
            buffer[^44] = 0xBC;
            buffer[^43] = 0xCA;
            buffer[^42] = 0xB3;
            buffer[^41] = 0x97;
            buffer[^40] = 0xDF;
            buffer[^39] = 0xDF;
            buffer[^38] = 0xDF;
            buffer[^37] = 0xAD;
            buffer[^36] = 0x4B;
            buffer[^35] = 0x4F;
            buffer[^34] = 0x07;
            buffer[^33] = 0x23;
            buffer[^32] = 0x6B;
            buffer[^31] = 0x6B;
            buffer[^30] = 0x6B;
            buffer[^29] = 0x19;
            buffer[^28] = 0xFF;
            buffer[^27] = 0xFB;
            buffer[^26] = 0xB3;
            buffer[^25] = 0x97;
            buffer[^24] = 0xDF;
            buffer[^23] = 0x0D;
            buffer[^22] = 0x19;
            buffer[^21] = 1;
            buffer[^18] = 0x10;
            buffer[^17] = 1;
            buffer[^2] = 1;
        }
    }
}