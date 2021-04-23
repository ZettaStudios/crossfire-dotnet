using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using crossfire_server.network;
using crossfire_server.session;
using crossfire_server.util;

namespace crossfire_server.server
{
    public class Server
    {
        protected string name = "Base Server";
        protected string token = "Zetta@123";
        protected string address = "0.0.0.0";
        protected short port = 13008;
        protected Thread thread;
        protected ArrayList sessions = new ArrayList();
        protected Network network;

        public Server(string[] args)
        {
            if (args.Length > 0)
            {
                port = short.Parse(args[0]);
            }
        }
        
        public virtual void Start() {
            thread = new Thread(() =>
            {
                Thread.CurrentThread.Name = name;
                Log("Loading...");
                try {
                    IPAddress ipAddress = IPAddress.Parse(address);
                    TcpListener server = new TcpListener(ipAddress, port);
                    server.Start();
                    Log($"Listening at {ipAddress}:{port}.");
                    while (true) {
                        onRun(server.AcceptTcpClient());
                    }
                } catch (IOException e) {
                    Log(e.StackTrace);
                }
            });
            thread.Start();
        }

        public void Stop()
        {
            Log($"STOP RECEIVED, CLOSING ALL SESSIONS [{sessions.Count}].");
            for (int i = 0; i < sessions.Count; i++)
            {
                ((Session) sessions[i]).Close();
            }
            thread.Interrupt();
            Log("ALL SESSIONS HAS BEEN CLOSED AND SERVER STOPPED.");
        }

        public virtual void onRun(TcpClient client)
        {
            
        }

        public virtual void GetServerInfo() {
            Log(string.Format("Sessions: {0}.", sessions.Count));
        }
        
        public void Log(string message) {
            Console.Log($"[{name}] {message}");
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Address
        {
            get => address;
            set => address = value;
        }

        public short Port
        {
            get => port;
            set => port = value;
        }

        public string Token => token;

        public Thread Thread => thread;

        public ArrayList Sessions => sessions;

        public Network Network => network;
    }
}