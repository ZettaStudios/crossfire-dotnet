using System;
using System.Net.Sockets;
using crossfire_server.enums;
using crossfire_server.network.login.packet;
using crossfire_server.server;
using crossfire_server.util;
using crossfire_server.util.log.Factories;
using DataPacket = crossfire_server.network.DataPacket;

namespace crossfire_server.session
{
    public class LoginSession : Session
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
            int connected = 1;
            
            if (TestUser.exists && TestUser.username == packet.Username && TestUser.password == packet.Password && connected == 0)
            {
                Authenticate(LoginErrorsType.NoError, packet);
            }
            else if (TestUser.exists && TestUser.username == packet.Username && TestUser.password == packet.Password)
            {
                Authenticate(LoginErrorsType.PlayerAlreadyLoggedIn, packet);
            }
            else
            {
                Authenticate(LoginErrorsType.UnknownUsernameOrPassword, packet);
            }
        }

        private void Authenticate(LoginErrorsType type, LoginRequestDataPacket request)
        {
            if (type == LoginErrorsType.NoError)
            {
                LogFactory.GetLog(server.Name).LogInfo($"[SESSION] [AUTHENTICATE STATUS: {type.ToString()}].");
            }
            else
            {
                LoginErrorResponsePacket packet = new LoginErrorResponsePacket {Identifier = 0, Error = type};
                SendPacket(packet);
                LogFactory.GetLog(server.Name).LogInfo($"[SESSION] [AUTHENTICATE STATUS: {type.ToString()}].");
            }
        }
    }
}