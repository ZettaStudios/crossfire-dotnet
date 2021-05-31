using System;
using System.Net.Sockets;
using Login.Enum;
using Login.Network.packet;
using Shared;
using Shared.Network;
using Shared.Util;
using Shared.Util.Log.Factories;

namespace Login.Session
{
    public class LoginSession : Shared.Session.Session
    {
        public LoginSession(Server server, TcpClient client) : base(server, client)
        {
        }
        protected override void onRun(byte[] buffer)
        {
            try
            {
                DataPacket packet = server.Network.GetPacket((short)server.Network.GetTypeOf(buffer));
                if (packet != null)
                {
                    packet.SetBuffer(buffer);
                    if (packet.IsValid)
                    {
                        packet.Decode();
                        LogFactory.GetLog(server.Name).LogInfo($"Received Packet [{packet.Pid().ToString()}] [{packet.Buffer.Length}]");
                        LogFactory.GetLog(server.Name).LogInfo($"\n{NetworkUtil.DumpPacket(packet.Buffer)}");
                        HandlePacket(packet);
                    }
                    else
                    {
                        LogFactory.GetLog(server.Name).LogWarning($"Received Invalid Packet [{packet.Pid().ToString()}] [{packet.Buffer.Length}]");
                        packet.Decode();
                        LogFactory.GetLog(server.Name).LogInfo($"\n{NetworkUtil.DumpPacket(packet.Buffer)}");
                    }
                }
                else
                {
                    LogFactory.GetLog(server.Name).LogWarning("Unknown Packet.");
                } 
                base.onRun(buffer);
            }catch (Exception e){
                LogFactory.GetLog(server.Name).LogFatal(e);
            }
        }

        private void HandlePacket(DataPacket packet)
        {
            switch (packet.Pid())
            {
                case LoginRequestDataPacket.NetworkId:
                    LoginRequestDataPacket loginRequestDataPacket = (LoginRequestDataPacket) packet;
                    Validate(loginRequestDataPacket);
                    break;
            }
        }

        private void Validate(LoginRequestDataPacket packet)
        {
            int connected = 0;
            
            if (TestUser.exists && TestUser.username == packet.Username && TestUser.password == packet.Password && connected == 0)
            {
                Authenticate(ErrorsType.NoError, packet);
            }
            else if (TestUser.exists && TestUser.username == packet.Username && TestUser.password == packet.Password)
            {
                Authenticate(ErrorsType.PlayerAlreadyLoggedIn, packet);
            }
            else
            {
                Authenticate(ErrorsType.UnknownUsernameOrPassword, packet);
            }
        }

        private void Authenticate(ErrorsType type, LoginRequestDataPacket request)
        {
            if (type == ErrorsType.NoError)
            {
                LoginResponsePacket packet = new LoginResponsePacket();
                SendPacket(packet);
                LogFactory.GetLog(server.Name).LogInfo($"[SESSION] [AUTHENTICATE STATUS: {type.ToString()}].");
            }
            else
            {
                LoginErrorResponsePacket packet = new LoginErrorResponsePacket {Identifier = 0, Error = type};
                SendPacket(packet);
                LogFactory.GetLog(server.Name).LogInfo($"[SESSION] [AUTHENTICATE STATUS: {type.ToString()}].");
            }
        }

        public override void OnFinishPacketSent(DataPacket packet)
        {
            switch (packet.Pid())
            {
                case (short) PacketType.S2CValidAccount:
                    SendServerListPacket response = new SendServerListPacket();
                    SendPacket(response);
                    break;
            }
            base.OnFinishPacketSent(packet);
        }
    }
}