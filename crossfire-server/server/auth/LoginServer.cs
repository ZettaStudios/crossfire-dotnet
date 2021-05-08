using System.Net.Sockets;
using crossfire_server.enums;
using crossfire_server.network.login;
using crossfire_server.network.login.packet;
using crossfire_server.session;

namespace crossfire_server.server
{
    public class LoginServer : Server
    {
        public LoginServer(string[] args) : base(args)
        {
            name = "Login Server";
            type = ServerType.Authentication;
            port = 13008;
            network = new LoginNetwork();
        }

        public override void onRun(TcpClient client)
        {
            if (sessions.Count < maxConnections)
            {
                LoginSession session = new LoginSession(this, client);
                session.Start();
            }
            else
            {
                client.Close();
            }
            base.onRun(client);
        }
    }
}