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

        public override void onRun(byte[] buffer)
        {
            try
            {
                DataPacket packet = server.Network.GetPacket((short)server.Network.GetTypeOf(buffer));
                if (packet != null)
                {
                    packet.SetBuffer(buffer);
                    if (packet.IsValid)
                    {
                        LogFactory.GetLog("Main").LogInfo($"Received Packet [{packet.Pid().ToString()}] [{buffer.Length}]");
                        LogFactory.GetLog("Main").LogInfo(NetworkUtil.DumpPacket(buffer));
                        packet.Decode();
                        HandlePacket(packet);
                    }
                    else
                    {
                        LogFactory.GetLog("Main").LogWarning($"Received Invalid Packet [{packet.Pid().ToString()}] [{buffer.Length}]");
                    }
                }
                else
                {
                    LogFactory.GetLog("Main").LogWarning("Unknown Packet.");
                }
            
            base.onRun(buffer);
            }catch (Exception e){
                LogFactory.GetLog("Main").LogFatal(e);
            }
        }
        
        public void HandlePacket(DataPacket packet)
        {
            switch (packet.Pid())
            {
                case LoginRequestDataPacket.NetworkId:
                    LoginRequestDataPacket loginRequestDataPacket = (LoginRequestDataPacket) packet;
                    Validate(loginRequestDataPacket);
                    break;
            }
        }
        
        public void Validate(LoginRequestDataPacket packet)
        {
            Random random = new Random();
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
        
        public void Authenticate(LoginErrorsType type, LoginRequestDataPacket request)
        {
            if (type == LoginErrorsType.NoError)
            {
                LogFactory.GetLog("Main").LogInfo($"[SESSION] [AUTHENTICATE STATUS: {type.ToString()}].");
            }
            else
            {
                LoginErrorResponsePacket packet = new LoginErrorResponsePacket {Identifier = 0, Error = type};
                SendPacket(packet);
                LogFactory.GetLog("Main").LogInfo($"[SESSION] [AUTHENTICATE STATUS: {type.ToString()}].");
            }
        }
    }
}