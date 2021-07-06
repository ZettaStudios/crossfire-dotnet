using Newtonsoft.Json;

namespace Shared.Model
{
    public class UserData
    {
        [JsonProperty("status")] 
        public int Status;
        [JsonProperty("data")] 
        public User User;
    }
}