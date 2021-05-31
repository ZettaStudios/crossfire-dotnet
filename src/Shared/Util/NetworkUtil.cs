using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Shared.Util {
    public class NetworkUtil {
        public static string MacPattern = "%02X%s";

        public static string ParseMac(byte[] mac) {
            var result = "";
            for (var k = 0; k < mac.Length; k++)
                result += string.Format(MacPattern, mac[k], k < mac.Length - 1 ? ":" : "");
            return result;
        }

        public static byte[] StringToBytes(string str, int BufferLength = 0, bool endsWithF2 = false) {
            if (BufferLength != 0) {
                var result = str.Split(new[] {' '});
                var temp = new List<byte>();
                for (var i = 0; i < result.Length; i++) temp.Add(byte.Parse(result[i], NumberStyles.HexNumber));
                var oldCount = temp.Count;
                for (var i = 0; i < BufferLength - oldCount; i++) temp.Add(0);
                if (endsWithF2)
                    temp.ToArray()[temp.ToArray().Length - 1] = 0xF2;
                return temp.ToArray();
            } else {
                var result = str.Split(new[] {' '});
                var temp = new List<byte>(1024);
                for (var i = 0; i < result.Length; i++) temp.Add(byte.Parse(result[i], NumberStyles.HexNumber));
                return temp.ToArray();
            }
        }

        public static string BytesToString(byte[] buffer) {
            var builder = new StringBuilder();
            foreach (var b in buffer) {
                builder.Append(b.ToString("X2"));
                builder.Append(" ");
            }

            return builder.Remove(builder.Length - 1, 1).ToString();
        }

        public static string DumpPacket(byte[] packet) {
            var DataStr = "";
            var PacketLength = (ushort) packet.Length;
            for (var i = 0; i < Math.Ceiling((double) PacketLength / 16); i++) {
                var t = 16;
                if ((i + 1) * 16 > PacketLength)
                    t = PacketLength - i * 16;
                for (var a = 0; a < t; a++) DataStr += packet[i * 16 + a].ToString("X2") + " ";
                if (t < 16)
                    for (var a = t; a < 16; a++)
                        DataStr += "   ";
                DataStr += "     ;";

                for (var a = 0; a < t; a++) DataStr += Convert.ToChar(packet[i * 16 + a]);
                DataStr += Environment.NewLine;
            }

            DataStr.Replace(Convert.ToChar(0), '.');

            return DataStr.ToUpper();
        }
    }
}