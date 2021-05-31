namespace Shared.Network
{
    public abstract class DataPacket : NetworkWriter
    {
        protected const short NetworkId = -1;
        protected internal const byte StartsWith = 0xF1;
        protected internal const byte EndsWith = 0xF2;

        public abstract short Pid();
        
        public abstract void Decode();

        public abstract void Encode();

        public bool IsValid
        {
            get { return buffer[0] == StartsWith && buffer[buffer.Length - 1] == EndsWith; }
        }
    }
}