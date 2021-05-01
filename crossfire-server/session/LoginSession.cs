using System;
using System.Net.Sockets;
using crossfire_server.enums;
using crossfire_server.network.login.packet;
using crossfire_server.server;
using crossfire_server.util;
using crossfire_server.util.log.Factories;
//using Console = crossfire_server.util.Console;
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
            DataPacket packet = server.Network.GetPacket((short) server.Network.GetTypeOf(buffer));
            if (packet != null)
            {
                packet.SetBuffer(buffer);
                if (packet.IsValid)
                {
                    server.Log($"Received Packet [{packet.Pid().ToString()}] [{buffer.Length}]");
                    LogFactory.GetLog("Main").LogInfo(NetworkUtil.DumpPacket(buffer));
                    packet.Decode();
                    HandlePacket(packet);
                }
                else
                {
                    server.Log($"Received Invalid Packet [{packet.Pid().ToString()}] [{buffer.Length}]");
                }
            }
            else
            {
                server.Log("Unknown Packet.");
            }
            base.onRun(buffer);
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
                server.Log($"[SESSION] [AUTHENTICATE STATUS: {type.ToString()}].");
            }
            else
            {
                LoginErrorResponsePacket packet = new LoginErrorResponsePacket {Identifier = 0, Error = type};
                SendPacket(packet);
                server.Log($"[SESSION] [AUTHENTICATE STATUS: {type.ToString()}].");
            }
        }
    }
}