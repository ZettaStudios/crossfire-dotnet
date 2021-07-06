using Login.Enum;
using Login.Network.packet;

namespace Login.Network
{
    public class LoginNetwork : Shared.Network.Network
    {
        public LoginNetwork()
        {
            RegisterPackets();
        }
        protected sealed override void RegisterPackets()
        {
            RegisterPacket<LoginRequestDataPacket>((short) PacketType.C2SLogin);
            RegisterPacket<LoginExitRequestPacket>((short) PacketType.C2SExit);
            RegisterPacket<LoginToGameServerRequestStep1Packet>((short) PacketType.C2SLoginToGameServerStep1);
        }

        public override object GetTypeOf(byte[] buffer)
        {
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
                                            return PacketType.C2SLogin; 
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return PacketType.C2SGoBackForServers;
                                    }
                                    break;
                                }
                            case 8:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return PacketType.C2SCreateAccount;
                                    }
                                    break;
                                }
                            case 10:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return PacketType.C2SCheckNameExists;
                                    }
                                    break;
                                }
                            case 12:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return PacketType.C2SAccountAlreadyLoggedOn;
                                    }
                                    break;
                                }
                            case 15:
                                {
                                    switch (buffer[5])
                                    {
                                        case 0: 
                                            return PacketType.C2SLoginToGameServerStep1;
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
                                                        return PacketType.C2SExit;
                                                    case 1: 
                                                        return PacketType.C2SLoginToGameServerStep2;
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
                                            return PacketType.C2SRequestExit;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }

            return PacketType.Unknown;
        }
    }
}