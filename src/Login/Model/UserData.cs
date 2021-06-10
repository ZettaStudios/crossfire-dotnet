using Newtonsoft.Json;
using Shared.Model;

namespace Login.Model
{
    public class UserData
    {
        [JsonProperty("status")] 
        public int Status;
        [JsonProperty("data")] 
        public User User;
    }
}