using System.Net.Sockets;
using Login.Network;
using Login.Session;
using Shared;
using Shared.Enum;

namespace Login {
    public class LoginServer : Server {
        public LoginServer(string[] args) : base(args)
        {
            name = "Login Server";
            type = ServerType.Authentication;
            port = 13008;
            maxConnections = 200;
            network = new LoginNetwork();
        }

        public override void OnRun(TcpClient client)
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
            base.OnRun(client);
        }
    }
}