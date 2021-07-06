using Newtonsoft.Json;

namespace Game.Model
{
    public class ServerSettingsRequest
    {
        [JsonProperty("status")] 
        public int Status;
        [JsonProperty("data")] 
        public Shared.Model.GameServer Server;
    }
}