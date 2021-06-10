using Newtonsoft.Json;

namespace Shared.Model
{
    public class User
    {
        [JsonProperty("id")]
        public uint Id;
        [JsonProperty("nickname")]
        public string Nickname;
        [JsonProperty("password")]
        public string Password;
        [JsonProperty("kills")]
        public uint Kills;
        [JsonProperty("deaths")]
        public uint Deaths;
        [JsonProperty("rank")]
        public ushort Rank;

        public bool Verify(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}