using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using crossfire_server.network;
using crossfire_server.server;
using crossfire_server.util.log.Factories;

namespace crossfire_server.session
{
    public class Session
    {
        protected string id;
        protected Server server;
        protected TcpClient client;
        protected Thread thread;
        protected bool isRunning;
        protected NetworkStream NetworkStream;

        protected Queue<DataPacket> _packetQueue = new Queue<DataPacket>();
        protected int MAX_BUFFER_SIZE = 4096;

        public Session(Server server, TcpClient client)
        {
            id = Guid.NewGuid().ToString(); // Temp ID
            this.server = server;
            this.client = client;
            thread = new Thread(Run);
            server.Sessions.Add(this);
            isRunning = thread.IsAlive;
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
                LogFactory.GetLog(server.Name).LogInfo($"[CLOSED SESSION] [ID: {id}] [{client.Client.RemoteEndPoint}].");
                client.Close();
            } catch (ObjectDisposedException e) 
            {
                LogFactory.GetLog(server.Name).LogInfo($"[CLOSED WITH EXCEPTION] [ID: {id}] [{e.Message}].");
            }
            thread.Interrupt();
        }
        public void SendPacket(DataPacket packet)
        {
            _packetQueue.Enqueue(packet);
        }

        private bool TryDequeuePacket(out DataPacket packet)
        {
            packet = null;
            if (_packetQueue.Count != 0)
                packet = _packetQueue.Dequeue();
            return packet != null;
        }
        
        private void TrySend()
        {
            DataPacket packet;
            while (TryDequeuePacket(out packet))
            {
                try
                {
                    packet.Encode();
                    client.Client.BeginSend(packet.Buffer, 0, packet.Buffer.Length, SocketFlags.None,
                        CompletePacketSend, packet);
                }
                catch (Exception e)
                {
                    LogFactory.GetLog(server.Name).LogError($"[PACKET SEND] [ERROR] [MSG:{e.Message}]");
                    Close();
                }
            }
        }
        
        private void CompletePacketSend(IAsyncResult ar)
        {
            if (ar.AsyncState is DataPacket packet)
            {
                client.Client.EndSend(ar);
                LogFactory.GetLog(server.Name).LogInfo($"Packet Sent [{packet.Pid().ToString()}] [{packet.Buffer.Length}] to [{id}].");
            }
        }

        private void Run()
        {
            try {
                LogFactory.GetLog(server.Name).LogInfo($"[NEW SESSION] [ID: {id}] [{client.Client.RemoteEndPoint}].");
                while (true)
                {
                    if (!client.Connected) return;
                    NetworkStream = client.GetStream();
                    byte[] buffer = new byte[client.ReceiveBufferSize];
                    NetworkStream.Read(buffer, 0, buffer.Length);
                    onRun(buffer);
                    TrySend();
                }
            } catch (IOException e) {
                if (e.Message == null)
                    return;
                if ((e.HResult & 0x0000FFFF) == 5664) {
                    try {
                        Close();
                    } catch (IOException ex) {
                        LogFactory.GetLog(server.Name).LogFatal(ex);
                    }
                }
            }
        }

        public virtual void onRun(byte[] bytes) {}
        
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
                LogFactory.GetLog(server.Name).LogInfo($"[SESSION [{id}]] ID has been changed to [{value}].");
                id = value;
            }
        }

        public Thread Thread => thread;

        public int MaxBufferSize => MAX_BUFFER_SIZE;

        public bool IsRunning => isRunning;
    }
}