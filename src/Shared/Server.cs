using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Shared.Enum;
using Shared.Util.Log.Factories;

namespace Shared
{
    public class Server
    {
        protected string name = "Base Server";
        protected ServerType type = ServerType.Unknown;
        protected int maxConnections = 500;
        protected string token = "Zetta@123";
        protected string address = "127.0.0.1";
        protected short port = 13008;
        protected Thread thread;
        protected ArrayList sessions = new ArrayList();
        protected Network.Network network;
        private bool _alive = false;

        public Server(string[] args)
        {
            if (args.Length > 0)
            {
                port = short.Parse(args[0]);
            }
        }
        
        public virtual void Start()
        {
            thread = new Thread(() =>
            {
                Thread.CurrentThread.Name = name;
                LogFactory.GetLog(name).LogInfo("Loading...");
                try {
                    IPAddress ipAddress = IPAddress.Parse(address);
                    TcpListener server = new TcpListener(ipAddress, port);
                    server.Start();
                    LogFactory.GetLog(name).LogInfo($"Listening at {ipAddress}:{port}.");
                    while (IsAlive)
                    {
                        if (server.Pending())
                        {
                            server.BeginAcceptTcpClient(OnReceiveConnection, server);
                        }
                    }
                } catch (IOException e) {
                    LogFactory.GetLog(name).LogFatal(e);
                }
            });
            thread.Start();
            _alive = true;
        }

        private void OnReceiveConnection(IAsyncResult ar)
        {
            try
            {
                TcpListener server = (TcpListener) ar.AsyncState;
                if (server != null) OnRun(server.EndAcceptTcpClient(ar));
            }
            catch (Exception e)
            {
                LogFactory.GetLog(name).LogError(e.Message);
            }
        }

        public void Stop()
        {
            LogFactory.GetLog(name).LogWarning($"STOP RECEIVED, CLOSED ALL SESSIONS [{sessions.Count}].");
            for (int i = 0; i < sessions.Count; i++)
            {
                ((Session.Session) sessions[i])?.Close();
            }
            thread.Interrupt();
            LogFactory.GetLog(name).LogWarning("ALL SESSIONS HAS BEEN CLOSED AND SERVER STOPPED.");
            _alive = false;
        }

        public virtual void OnRun(TcpClient client)
        {
            
        }

        public virtual void GetServerInfo() {
            LogFactory.GetLog(name).LogInfo($"Sessions: {sessions.Count} of {maxConnections}.");
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

        public Network.Network Network => network;

        public ServerType Type
        {
            get => type;
            set => type = value;
        }

        public bool IsAlive => _alive;
    }
}