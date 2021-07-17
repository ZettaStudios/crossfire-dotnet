using Newtonsoft.Json;

namespace Shared.Model
{
    public class BattleStatistics
    {
        [JsonProperty("wins")]
        public uint Wins;
        [JsonProperty("loses")]
        public uint Loses;
        [JsonProperty("desertion")]
        public uint Desertion;
        [JsonProperty("kills")]
        public uint Kills;
        [JsonProperty("deaths")]
        public uint Deaths;
        [JsonProperty("assists")]
        public uint Assists;
        [JsonProperty("headshots")]
        public uint Headshots;
        [JsonProperty("granade")]
        public uint Granade;
        [JsonProperty("knife")]
        public uint Knife;
    }
}