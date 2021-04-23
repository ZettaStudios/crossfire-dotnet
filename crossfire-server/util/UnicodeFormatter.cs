using System;
using System.Text;

namespace crossfire_server.util
{
    public class UnicodeFormatter
    {
        static public string byteToHex(byte b)
        {
            byte[] data = { b };
            return BitConverter.ToString(data);
        }
        
        static public string byteToHex(byte[] data)
        {
            return BitConverter.ToString(data);
        }

        static public string charToHex(char c) {
            char[] data = { c };
            byte hi = Encoding.ASCII.GetBytes(data)[0];
            byte lo = (byte) (c & 0xff);
            return byteToHex(hi) + byteToHex(lo);
        }
    }
}