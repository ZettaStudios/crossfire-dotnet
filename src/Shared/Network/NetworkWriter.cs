using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Network
{
    public class NetworkWriter 
    {
        protected byte[] buffer = new byte[2048];

        public byte[] Buffer
        {
            get => buffer;
        }

        public void SetBuffer(byte[] value)
        {
            buffer = new byte[value.Length + 7];
            buffer[0] = DataPacket.StartsWith;
            buffer[buffer.Length - 1] = DataPacket.EndsWith;
            for (int i = 6; i < (value.Length + 6); i++)
            {
                buffer[i] = value[i - 6];
            }
            Write((ushort) buffer.Length, 1);
        }
        
        public void SetBuffer(byte[] value, int optionalLength)
        {
            buffer = new byte[value.Length + 7];
            buffer[0] = DataPacket.StartsWith;
            buffer[buffer.Length - 1] = DataPacket.EndsWith;
            for (int i = 6; i < (value.Length + 6); i++)
            {
                buffer[i] = value[i - 6];
            }
            Write((ushort) optionalLength, 1);
        }

        public void Write(string arg, int offset)
        {
            if (buffer == null)
                return;
            if (offset > buffer.Length - 1)
                return;
            byte[] argEncoded = Encoding.Default.GetBytes(arg);
            if (buffer.Length >= offset + arg.Length)
                Array.Copy(argEncoded, 0, buffer, offset, arg.Length);
        }
        
        public byte[] Write(string arg, int offset, byte[] data)
        {
            if (data == null)
                return new byte[2048];
            if (offset > data.Length - 1)
                return new byte[2048];
            byte[] argEncoded = Encoding.Default.GetBytes(arg);
            if (data.Length >= offset + arg.Length)
                Array.Copy(argEncoded, 0, data, offset, arg.Length);
            return data;
        }
        
        public byte[] Write(byte arg, int offset, byte[] data)
        {
            if (data == null)
                return new byte[2048];
            if (offset > data.Length - 1)
                return new byte[2048];
            data[offset] = arg;
            return data;
        }
        
        public void Write(byte arg, int offset)
        {
            if (buffer == null)
                return;
            if (offset > buffer.Length - 1)
                return;
            buffer[offset] = arg;
        }
        
        public void Write(bool arg, int offset)
        {
            if (buffer == null)
                return;
            if (offset > buffer.Length - 1)
                return;
            Write(arg ? (byte)1 : (byte)0, offset);
        }
        
        public void Write(ushort arg, int offset)
        {
            if (buffer == null)
                return;
            if (offset > buffer.Length - 1)
                return;
            if (buffer.Length >= offset + sizeof(ushort))
            {
                buffer[offset] = (byte)arg;
                buffer[offset + 1] = (byte)(arg >> 8);
            }
        }
        
        public void Write(uint arg, int offset)
        {
            if (buffer == null)
                return;
            if (offset > buffer.Length - 1)
                return;
            if (buffer.Length >= offset + sizeof(uint))
            {
                buffer[offset] = (byte)arg;
                buffer[offset + 1] = (byte)(arg >> 8);
                buffer[offset + 2] = (byte)(arg >> 16);
                buffer[offset + 3] = (byte)(arg >> 24);
            }
        }
        
        public byte[] Write(uint arg, int offset, byte[] data)
        {
            if (data == null)
                return new byte[2048];
            if (offset > data.Length - 1)
                return new byte[2048];
            if (data.Length >= offset + sizeof(uint))
            {
                data[offset] = (byte)arg;
                data[offset + 1] = (byte)(arg >> 8);
                data[offset + 2] = (byte)(arg >> 16);
                data[offset + 3] = (byte)(arg >> 24);
            }

            return data;
        }
        
        public void Write(ulong arg, int offset)
        {
            if (buffer == null)
                return;
            if (offset > buffer.Length - 1)
                return;
            if (buffer.Length >= offset + sizeof(ulong))
            {
                buffer[offset] = (byte)(arg);
                buffer[offset + 1] = (byte)(arg >> 8);
                buffer[offset + 2] = (byte)(arg >> 16);
                buffer[offset + 3] = (byte)(arg >> 24);
                buffer[offset + 4] = (byte)(arg >> 32);
                buffer[offset + 5] = (byte)(arg >> 40);
                buffer[offset + 6] = (byte)(arg >> 48);
                buffer[offset + 7] = (byte)(arg >> 56);
            }
        }
        
        public void Write(int arg, int offset)
        {
            if (buffer == null)
            {
                return;
            }
            if (offset > buffer.Length - 1)
            {
                return;
            }
            if (buffer.Length >= offset + sizeof(uint))
            {
                buffer[offset] = (byte)(arg);
                buffer[offset + 1] = (byte)(arg >> 8);
                buffer[offset + 2] = (byte)(arg >> 16);
                buffer[offset + 3] = (byte)(arg >> 24);
            }
        }
        
        public void Write(List<string> arg, int offset)
        {
            if (arg == null)
                return;
            if (buffer == null)
                return;
            if (offset > buffer.Length - 1)
                return;
            buffer[offset] = (byte)arg.Count;
            offset++;
            foreach (string str in arg)
            {
                buffer[offset] = (byte)str.Length;
                Write(str, offset + 1);
                offset += str.Length + 1;
            }
        }
        
        public void Write(string[] arg, int offset)
        {
            if (arg == null)
                return;
            if (buffer == null)
                return;
            if (offset > buffer.Length - 1)
                return;
            buffer[offset] = (byte)arg.Length;
            offset++;
            foreach (string str in arg)
            {
                buffer[offset] = (byte)str.Length;
                Write(str, offset + 1);
                offset += str.Length + 1;
            }
        }
        
        public void Write(byte[] arg, int offset)
        {
            if (arg == null)
                return;
            if (buffer == null)
                return;
            if (offset > buffer.Length - 1)
                return;
            System.Buffer.BlockCopy(arg, 0, buffer, offset, arg.Length);
        }
        
        public void WriteWithLength(string arg, int offset)
        {
            if (buffer == null)
            {
                return;
            }
            if (offset > buffer.Length - 1)
            {
                return;
            }
            int till = buffer.Length - offset;
            till = Math.Min(arg.Length, till);
            buffer[offset] = (byte)arg.Length;
            offset++;
            ushort i = 0;
            var bytes = Encoding.Default.GetBytes(arg);
            while (i < till)
            {
                buffer[(ushort)(i + offset)] = bytes[i];
                i = (ushort)(i + 1);
            }
        }

        public String ToString(int index, int count)
        {
            return Encoding.ASCII.GetString(buffer, index, count)
                .Replace(Encoding.ASCII.GetString(new byte[] {0x0}), "");
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}