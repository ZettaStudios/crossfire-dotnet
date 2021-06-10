using Newtonsoft.Json;
using Shared.Enum;

namespace Shared.Model
{
    public class GameServer
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("port")]
        public ushort Port;
        [JsonProperty("players")]
        public uint PlayersOnline;
        [JsonProperty("limit")]
        public uint Limit;
        [JsonProperty("address")]
        public string Address;
        [JsonProperty("nolimit")]
        public ushort NoLimit;
        [JsonProperty("minrank")]
        public ushort MinRank;
        [JsonProperty("maxrank")]
        public ushort MaxRank;
        [JsonProperty("type")]
        public ServerType Type;
    }
}