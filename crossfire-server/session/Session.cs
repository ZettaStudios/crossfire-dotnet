using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using crossfire_server.network;
using crossfire_server.server;
using Console = crossfire_server.util.Console;

namespace crossfire_server.session
{
    public class Session
    {
        protected string id;
        protected Server server;
        protected TcpClient client;
        protected Thread thread;
        protected int MAX_BUFFER_SIZE = 2047;
        protected bool isRunning;
        protected NetworkStream networkStream;
        protected byte[] buffer;

        public Session(Server server, TcpClient client)
        {
            id = Guid.NewGuid().ToString(); // Temp ID
            this.server = server;
            this.client = client;
            thread = new Thread(Run);
            server.Sessions.Add(this);
            isRunning = thread.IsAlive;
            networkStream = client.GetStream();
            buffer = new byte[MAX_BUFFER_SIZE];
        }

        public virtual void Start()
        {
            thread.Start();
        }

        public virtual void Close()
        {
            server.Sessions.Remove(this);
            try
            {
                server.Log($"[CLOSED SESSION] [ID: {id}] [{client.Client.RemoteEndPoint}].");
                client.Close();
            } catch (ObjectDisposedException e) 
            {
                server.Log($"[CLOSED WITH EXCEPTION] [ID: {id}] [{e.Message}].");
            }
            thread.Interrupt();
        }
        public virtual void SendPacket(DataPacket packet)
        {
            packet.Encode();
            try {
                if (networkStream.CanWrite)
                {
                    byte[] buffered = packet.Buffer;
                    client.Client.Send(buffered);
                    server.Log($"Packet Sent [{packet.Pid().ToString()}] [{buffered.Length}].");
                }
                else
                {
                    Close();
                }
            } catch (Exception e) {
                if (e is SocketException || e is IOException)
                {
                    server.Log($"[ERROR] {e.Message}.");
                }
            }
        }
        
        private void Run()
        {
            try {
                server.Log($"[NEW SESSION] [ID: {id}] [{client.Client.RemoteEndPoint}].");
                while (true)
                {
                    if (!client.Connected) return;
                    networkStream.Read(buffer, 0, buffer.Length);
                    onRun(buffer);
                }
            } catch (IOException e) {
                if (e.Message == null)
                    return;
                if ((e.HResult & 0x0000FFFF) == 5664) {
                    try {
                        Close();
                    } catch (IOException ex) {
                        server.Log(ex.Message);
                    }
                }
            }
        }

        public virtual void onRun(byte[] bytes)
        {
            
        }
        
        public Server Server
        {
            get => server;
            set => server = value;
        }

        public TcpClient Client
        {
            get => client;
            set => client = value;
        }

        public string Id
        {
            get => id;
            set
            {
                server.Log($"[SESSION [{id}]] ID has been changed to [{value}].");
                id = value;
            }
        }

        public Thread Thread => thread;

        public int MaxBufferSize => MAX_BUFFER_SIZE;

        public bool IsRunning => isRunning;
    }
}