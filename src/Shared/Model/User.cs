using Newtonsoft.Json;

namespace Shared.Model
{
    public class User
    {
        [JsonProperty("id")]
        public uint Id;
        [JsonProperty("lastguid")]
        public string Identifier;
        [JsonProperty("name")]
        public string Nickname;
        [JsonProperty("password")]
        public string Password;
        [JsonProperty("type")]
        public uint Type;
        [JsonProperty("kills")]
        public uint Kills;
        [JsonProperty("deaths")]
        public uint Deaths;
        [JsonProperty("level")]
        public ushort Rank;
        [JsonProperty("gp")]
        public int GamePoints;
        [JsonProperty("zp")]
        public int ZettaPoints;
        [JsonProperty("tutorial_done")]
        public bool TutorialDone;
        [JsonProperty("coupons_owned")] 
        public int CouponsOwned;

        public bool Verify(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}