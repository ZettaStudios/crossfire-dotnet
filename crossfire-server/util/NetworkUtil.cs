using System;
using System.Collections.Generic;
using System.Text;

namespace crossfire_server.util
{
    public class NetworkUtil
    {
        public static String MacPattern = "%02X%s";
        public static string ParseMac(byte[] mac)
        {
            string result = "";
            for (int k = 0; k < mac.Length; k++)
            {
                result += string.Format(MacPattern, mac[k], (k < mac.Length - 1) ? ":" : "");
            }
            return result;
        }
        
        public static byte[] StringToBytes(string str, int BufferLength = 0, bool endsWithF2 = false)
        {
            if (BufferLength != 0)
            {
                string[] result = str.Split(new char[] { ' ' });
                List<byte> temp = new List<byte>();
                for (int i = 0; i < result.Length; i++)
                {
                    temp.Add(byte.Parse((result[i]), System.Globalization.NumberStyles.HexNumber));
                }
                int oldCount = temp.Count;
                for (int i = 0; i < (BufferLength - oldCount); i++)
                {
                    temp.Add((byte)(0));
                }
                if (endsWithF2)
                    temp.ToArray()[temp.ToArray().Length - 1] = 0xF2;
                return temp.ToArray();
            }
            else
            {
                string[] result = str.Split(new char[] { ' ' });
                List<byte> temp = new List<byte>(1024);
                for (int i = 0; i < result.Length; i++)
                {
                    temp.Add(byte.Parse((result[i]), System.Globalization.NumberStyles.HexNumber));
                }
                return temp.ToArray();
            }
        }
        
        public static string BytesToString(byte[] buffer)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var b in buffer)
            {
                builder.Append(b.ToString("X2"));
                builder.Append(" ");
            }
            return builder.Remove(builder.Length - 1, 1).ToString();
        }
        
        public static string DumpPacket(byte[] packet)
        {
            string DataStr = "";
            ushort PacketLength = (ushort)packet.Length;
            for (int i = 0; i < Math.Ceiling((double)PacketLength / 16); i++)
            {
                int t = 16;
                if (((i + 1) * 16) > PacketLength)
                    t = PacketLength - (i * 16);
                for (int a = 0; a < t; a++)
                {
                    DataStr += packet[i * 16 + a].ToString("X2") + " ";
                }
                if (t < 16)
                    for (int a = t; a < 16; a++)
                        DataStr += "   ";
                DataStr += "     ;";

                for (int a = 0; a < t; a++)
                {
                    DataStr += Convert.ToChar(packet[i * 16 + a]);
                }
                DataStr += Environment.NewLine;
            }
            DataStr.Replace(Convert.ToChar(0), '.');

            return DataStr.ToUpper();

        }
    }
}