using Newtonsoft.Json;

namespace Shared.Model
{
    public class BattleData
    {
        [JsonProperty("status")] 
        public int Status;
        [JsonProperty("data")] 
        public BattleStatistics Statistics;
    }
}