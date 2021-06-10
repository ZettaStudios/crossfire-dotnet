using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Shared.Enum;
using Shared.Network;
using Shared.Util.Log.Factories;

namespace Shared.Session
{
    public class Session
    {
        protected string id;
        protected Server server;
        protected TcpClient client;
        protected Thread thread;
        protected bool isRunning;
        protected NetworkStream NetworkStream;

        protected Queue<DataPacket> _packetQueue = new Queue<DataPacket>(10);
        protected int MAX_BUFFER_SIZE = 1024 * 4;
        protected int TIMEOUT_VALUE = 120;

        public Session(Server server, TcpClient client)
        {
            id = Guid.NewGuid().ToString(); // Temp ID
            this.server = server;
            this.client = client;
            this.client.NoDelay = true;
            this.client.ReceiveTimeout = TIMEOUT_VALUE;
            this.client.SendTimeout = TIMEOUT_VALUE;
            thread = new Thread(Run);
            server.Sessions.Add(this);
        }

        public virtual void Start()
        {
            thread.Start();
            isRunning = client.Client.Connected;
        }

        public virtual void Close()
        {
            server.Sessions.Remove(this);
            try
            {
                LogFactory.GetLog(server.Name).LogInfo($"[CLOSED SESSION] [ID: {id}] [{client.Client.RemoteEndPoint}].");
                client.Client.Shutdown(SocketShutdown.Both);
            } catch (ObjectDisposedException e) 
            {
                LogFactory.GetLog(server.Name).LogInfo($"[CLOSED WITH EXCEPTION] [ID: {id}] [{e.Message}] [{e.StackTrace}].");
            }
            thread.Interrupt();
        }

        protected virtual void TryReadPacket()
        {
            byte[] buffer = new byte[MAX_BUFFER_SIZE];
            NetworkStream.BeginRead(buffer, 0, buffer.Length, OnReceiveCallback, buffer);
        }

        private void OnReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if (client.Client.Connected)
                {
                    int length = NetworkStream.EndRead(ar);
                    if (length > 0)
                    {
                        byte[] buffer = new byte[length];
                        Array.Copy((Array) ar.AsyncState ?? throw new InvalidOperationException(), 0, buffer, 0, length);
                        OnRun(buffer);
                    }
                }
            }
            catch (Exception e)
            {
                LogFactory.GetLog(server.Name).LogError($"[PACKET RECEIVE] [ERROR] [MSG:{e.Message}]");
            }
        }

        public void SendPacket(DataPacket packet)
        {
            if(packet != null) _packetQueue.Enqueue(packet);
        }

        private bool TryDequeuePacket(out DataPacket packet)
        {
            packet = null;
            if (_packetQueue.Count != 0)
                packet = _packetQueue.Dequeue();
            return packet != null;
        }
        
        private void TrySendPacket()
        {
            while (TryDequeuePacket(out var packet))
            {
                try
                {
                    packet.Encode();
                    if (packet.IsValid || server.Type.Equals(ServerType.Other))
                    {
                        NetworkStream.BeginWrite(packet.Buffer, 0, packet.Buffer.Length,
                            CompletePacketSend, packet);
                    }
                }
                catch (Exception e)
                {
                    LogFactory.GetLog(server.Name).LogError($"[PACKET SEND] [ERROR] [MSG:{e.Message}]");
                }
            }
        }
        
        private void CompletePacketSend(IAsyncResult ar)
        {
            if (ar.AsyncState is DataPacket packet)
            {
                NetworkStream.EndWrite(ar);
                LogFactory.GetLog(server.Name).LogInfo($"Packet Sent [{packet.Pid().ToString()}] [{packet.Buffer.Length}] to [{id}].");
                OnFinishPacketSent(packet);
            }
        }

        public virtual void OnFinishPacketSent(DataPacket packet)
        {
            packet.Buffer = null;
        }

        protected virtual void HandlePacket(DataPacket packet)
        {
            
        }
        
        private void Run()
        {
            try {
                LogFactory.GetLog(server.Name).LogInfo($"[NEW SESSION] [ID: {id}] [{client.Client.RemoteEndPoint}].");
                NetworkStream = client.GetStream();
                while (true)
                {
                    if (!client.Client.Connected) break;
                    if (NetworkStream.DataAvailable)
                    {
                        TryReadPacket();
                    }

                    if (_packetQueue.Count > 0 && NetworkStream.CanRead)
                    {
                        TrySendPacket();
                    }
                }
                Close();
            } catch (IOException e) {
                if ((e.HResult & 0x0000FFFF) == 5664) {
                    try {
                        Close();
                    } catch (IOException ex) {
                        LogFactory.GetLog(server.Name).LogFatal(ex);
                    }
                }
            }
        }

        protected virtual void OnRun(byte[] buffer)
        {
        }
        
        public SocketAddress GetAddress()
        {
            return client.Client.RemoteEndPoint.Serialize();
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
                LogFactory.GetLog(server.Name).LogInfo($"[SESSION : {id}] ID has been changed to [{value}].");
                id = value;
            }
        }

        public Thread Thread => thread;

        public int MaxBufferSize => MAX_BUFFER_SIZE;

        public bool IsRunning => isRunning;
    }
}