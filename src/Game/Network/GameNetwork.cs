using Game.Enum;
using Game.Network.packet;

namespace Game.Network
{
    public class GameNetwork : Shared.Network.Network
    {
        public GameNetwork()
        {
            RegisterPackets();
        }
        protected sealed override void RegisterPackets()
        {
            RegisterPacket<AuthToChannelServerPacket>((short) PacketType.C2SAuthToChannelServer);
            RegisterPacket<ClientHeartBeatPacket>((short) PacketType.C2SHeartBeat);
            RegisterPacket<ZettaPointsPacket>((short) PacketType.C2SGetZp);
            RegisterPacket<GetChannelsRequestPacket>((short) PacketType.C2SGetChannels);
        }

        public override object GetTypeOf(byte[] buffer)
        {
            byte offset3 = buffer[3];
            byte offset4 = buffer[4];
            byte offset5 = buffer[5];
            switch (offset3)
            {
                case 0 when offset4 == 21 && offset5 == 0:
                    return PacketType.C2SRequestExit;
                case 1 when offset4 == 0 && offset5 == 0:
                    return PacketType.C2SAuthToChannelServer;
                case 1 when offset4 == 5 && offset5 == 0:
                    return PacketType.C2SServerTime;
                case 1 when offset4 == 10 && offset5 == 0:
                    return PacketType.C2SConfirmBack;
                case 1 when offset4 == 19 && offset5 == 1:
                    return PacketType.C2SAutoJoin;
                case 1 when offset4 == 19 && offset5 == 6:
                    return PacketType.C2SAutoJoinOptions;
                case 1 when offset4 == 23 && offset5 == 0:
                    return PacketType.C2SChangeSettings;
                case 1 when offset4 == 26 && offset5 == 0:
                    return PacketType.C2SExitGameInfoInto;
                case 1 when offset4 == 30 && offset5 == 0:
                    return PacketType.C2SGetChannels;
                case 1 when offset4 == 31 && offset5 == 0:
                    return PacketType.C2SChannelJoin;
                case 1 when offset4 == 33 && offset5 == 0:
                    return PacketType.C2SGetPlayersOnChannel;
                case 1 when offset4 == 35 && offset5 == 0:
                    return PacketType.C2SExitFromChannelToChannelsList;
                case 1 when offset4 == 50 && offset5 == 0:
                    return PacketType.C2SChannelData;
                case 1 when offset4 == 52 && offset5 == 0:
                    return PacketType.C2SCreateRoom;
                case 1 when offset4 == 54 && offset5 == 0:
                    return PacketType.C2SJoinToRoom;
                case 1 when offset4 == 64 && offset5 == 0:
                    return PacketType.C2SBackFromRoom;
                case 1 when offset4 == 92 && offset5 == 0:
                    return PacketType.C2SGetAnotherPlayerStats;
                case 1 when offset4 == 93 && offset5 == 0:
                    return PacketType.C2SGetPlayerStats;
                case 1 when offset4 == 128 && offset5 == 0:
                    return PacketType.C2SGetZp;
                case 1 when offset4 == 132 && offset5 == 0:
                    return PacketType.С2SEnterToShootingRoom;
                case 1 when offset4 == 169 && offset5 == 1:
                    return PacketType.C2SFeverUpdate;
                case 1 when offset4 == 169 && offset5 == 4:
                    return PacketType.C2SChannelsUpdate;
                case 1 when offset4 == 169 && offset5 == 6:
                    return PacketType.C2SFeverInfoUpdate;
                case 1 when offset4 == 171 && offset5 == 0:
                    return PacketType.C2SHeartBeat;
                case 1 when offset4 == 200 && offset5 == 0:
                    return PacketType.C2SStorageItems;
                case 10 when offset4 == 35 && offset5 == 0:
                    return PacketType.C2SMileage;
                default:
                    return PacketType.Unknown;
            }
        }
    }
}