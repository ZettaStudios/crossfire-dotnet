using System;
using System.Collections.Concurrent;

namespace crossfire_server.network
{
    public abstract class Network
    {
        protected ConcurrentDictionary<short, Type> PacketPool = new ConcurrentDictionary<short, Type>();

        public DataPacket GetPacket(short id) {
            if (PacketPool.ContainsKey(id))
            {
                return (DataPacket) Activator.CreateInstance(PacketPool[id]);
            }

            return null;
        }

        public void RegisterPacket<TDPacket>(short id) where TDPacket : DataPacket
        {
            
            Type dType = typeof(TDPacket);
            if (!PacketPool.ContainsKey(id))
            {
                PacketPool.TryAdd(id, dType);
            }
        }

        protected abstract void RegisterPackets();

        public abstract object GetTypeOf(byte[] buffer);
    }
}