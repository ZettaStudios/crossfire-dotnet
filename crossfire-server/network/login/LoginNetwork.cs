using crossfire_server.enums;
using crossfire_server.network.login.packet;

namespace crossfire_server.network.login
{
    public class LoginNetwork : Network
    {
        public LoginNetwork()
        {
            RegisterPackets();
        }
        protected override void RegisterPackets()
        {
            RegisterPacket<LoginRequestDataPacket>((short) LoginType.C2SLogin);
        }

        public override object GetTypeOf(byte[] buffer)
        {
            byte Offset3 = buffer[3];
            byte Offset4 = buffer[4];
            byte Offset5 = buffer[5];
            byte Offset6 = buffer[6];
            switch (buffer[3])
            {
                case 0:
                    {
                        switch (buffer[4])
                        {
                            case 0:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return LoginType.C2SLogin; 
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return LoginType.C2SGoBackForServers;
                                    }
                                    break;
                                }
                            case 8:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return LoginType.C2SCreateAccount;
                                    }
                                    break;
                                }
                            case 10:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return LoginType.C2SCheckNameExsitance;
                                    }
                                    break;
                                }
                            case 12:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return LoginType.C2SAccountAlreadyLoggedOn;
                                    }
                                    break;
                                }
                            case 15:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return LoginType.C2SLoginToGameServerStep1;
                                    }
                                    break;
                                }
                            case 17:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0:
                                            {
                                                switch (buffer[6])
                                                {
                                                    case 0: 
                                                        return LoginType.C2SExit;
                                                    case 1: 
                                                        return LoginType.C2SLoginToGameServerStep2;
                                                }
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 21:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return LoginType.C2SRequestExit;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }

            return LoginType.Unknown;
        }
    }
}