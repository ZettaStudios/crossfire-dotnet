using System;
using System.Net.Sockets;
using Game.Network.packet;
using Newtonsoft.Json;
using Shared;
using Shared.Model;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

namespace Game.Session
{
    public class GameSession : Shared.Session.Session
    {
        private User _user;

        public GameSession(Server server, TcpClient client) : base(server, client)
        {
        }

        protected override void OnRun(byte[] buffer)
        {
            try
            {
                DataPacket packet = server.Network.GetPacket((short) server.Network.GetTypeOf(buffer));
                if (packet != null)
                {
                    packet.Buffer = buffer;
                    if (packet.IsValid)
                    {
                        packet.Decode();
                        LogFactory.GetLog(server.Name)
                            .LogInfo($"Received Packet [{packet.Pid().ToString()}] [{packet.Buffer.Length}]");
                        LogFactory.GetLog(server.Name).LogInfo($"\n{NetworkUtil.DumpPacket(packet.Buffer)}");
                        HandlePacket(packet);
                    }
                    else
                    {
                        LogFactory.GetLog(server.Name)
                            .LogWarning(
                                $"Received Invalid Packet [{packet.Pid().ToString()}] [{packet.Buffer.Length}]");
                        packet.Decode();
                        LogFactory.GetLog(server.Name).LogInfo($"\n{NetworkUtil.DumpPacket(packet.Buffer)}");
                    }
                }
                else
                {
                    LogFactory.GetLog(server.Name)
                        .LogWarning($"Unknown Packet with ID {(short) server.Network.GetTypeOf(buffer)}.");
                }
            }
            catch (Exception e)
            {
                LogFactory.GetLog(server.Name).LogFatal(e);
            }

            base.OnRun(buffer);
        }

        protected override void HandlePacket(DataPacket packet)
        {
            switch (packet.Pid())
            {
                case AuthToChannelServerPacket.NetworkId:
                    AuthToChannelServerPacket authToChannelServerPacket = (AuthToChannelServerPacket) packet;
                    Id = authToChannelServerPacket.Identifier;
                    SendPacket(authToChannelServerPacket);
                    Internet.Get("http://localhost:3000/", $"user/{authToChannelServerPacket.Username}", result =>
                    {
                        UserData data = JsonConvert.DeserializeObject<UserData>(result);
                        if (data != null)
                        {
                            _user = data.User;
                            _user.Identifier = id;
                        }
                    }, error => {});
                    break;
                case ZettaPointsPacket.NetworkId:
                    ZettaPointsPacket zettaPointsPacket = (ZettaPointsPacket) packet;
                    zettaPointsPacket.User = _user;
                    SendPacket(zettaPointsPacket);
                    break;
                case GetChannelsRequestPacket.NetworkId:
                    GetChannelsRequestPacket getChannelsRequestPacket = (GetChannelsRequestPacket) packet;
                    getChannelsRequestPacket.Server = server;
                    SendPacket(getChannelsRequestPacket);
                    break;
            }

            base.HandlePacket(packet);
        }

        public override void OnFinishPacketSent(DataPacket packet)
        {
            switch (packet.Pid())
            {
                case AuthToChannelServerPacket.NetworkId:
                    PlayerDataPacket p = new PlayerDataPacket();
                    SendPacket(p);
                    break;
            }

            base.OnFinishPacketSent(packet);
        }

        public User User => _user;
    }
}