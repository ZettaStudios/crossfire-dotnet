using crossfire_server.enums;
using crossfire_server.util;
using crossfire_server.util.log.Factories;

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
            LogFactory.GetLog("Main").LogInfo("Username: " + Username);
            LogFactory.GetLog("Main").LogInfo("Password: " + Password);
            LogFactory.GetLog("Main").LogInfo("Arguments: " + Arguments);
            LogFactory.GetLog("Main").LogInfo("MacAddress: " + MacAddress);
            LogFactory.GetLog("Main").LogInfo("Identifier: " + Identifier);
        }
    }
}