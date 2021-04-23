using crossfire_server.enums;
using crossfire_server.util;

namespace crossfire_server.network.login.packet
{
    public class LoginRequestDataPacket : network.DataPacket
    {
        public const short NetworkId = (short) LoginType.C2SLogin;

        // Player Data From Client
        public string Identifier;
        public string Username;
        public string Password;
        public string Arguments;
        public string MacAddress;
        
        public override short Pid()
        {
            return NetworkId;
        }

        public override void Decode()
        {
            if (IsValid)
            {
                Username = ToString(16, 20);
                Password = ToString(37, 20);
                Arguments = ToString(145, 12);
                MacAddress = ToString(407, 12);
                Identifier = ToString(77, 32);
            }
        }

        public override void Encode()
        {
            
        }

        public void Debug()
        {
            Console.Log("Username: " + Username);
            Console.Log("Password: " + Password);
            Console.Log("Arguments: " + Arguments);
            Console.Log("MacAddress: " + MacAddress);
            Console.Log("Identifier: " + Identifier);
        }
    }
}