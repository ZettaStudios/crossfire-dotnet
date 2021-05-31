using System;
using System.Text;

namespace Shared.Util {
    public class UnicodeFormatter {
        public static string byteToHex(byte b) {
            byte[] data = {b};
            return BitConverter.ToString(data);
        }

        public static string byteToHex(byte[] data) {
            return BitConverter.ToString(data);
        }

        public static string charToHex(char c) {
            char[] data = {c};
            var hi = Encoding.ASCII.GetBytes(data)[0];
            var lo = (byte) (c & 0xff);
            return byteToHex(hi) + byteToHex(lo);
        }
    }
}