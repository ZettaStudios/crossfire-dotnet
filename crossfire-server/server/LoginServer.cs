using System.Net.Sockets;
using crossfire_server.network.login;
using crossfire_server.session;

namespace crossfire_server.server
{
    public class LoginServer : Server
    {
        public LoginServer(string[] args) : base(args)
        {
            name = "Login Server";
            port = 13008;
            network = new LoginNetwork();
        }

        public override void onRun(TcpClient client)
        {
            LoginSession session = new LoginSession(this, client);
            session.Start();
            base.onRun(client);
        }
    }
}