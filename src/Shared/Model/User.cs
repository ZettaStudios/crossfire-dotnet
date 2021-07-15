using Newtonsoft.Json;
using Shared.Enum;

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
        [JsonProperty("exp")]
        public uint Experience;
        [JsonProperty("gp")]
        public int GamePoints;
        [JsonProperty("zp")]
        public int ZettaPoints;
        [JsonProperty("tutorialdone")]
        public bool TutorialDone;
        [JsonProperty("couponsowned")] 
        public int CouponsOwned;

        public bool Verify(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        public Grades NextRank
        {
            get
            {
                if (Rank.Equals((ushort)Grades.Marshall))
                    return (Grades)Rank;
                return (Grades)(Rank + 1);
            }
        }
    }
}