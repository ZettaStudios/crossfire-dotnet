using Newtonsoft.Json;
using Shared.Constants;
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
        [JsonProperty("assists")]
        public uint Assists;
        [JsonProperty("kills")]
        public uint Kills;
        [JsonProperty("deaths")]
        public uint Deaths;
        [JsonProperty("battles")]
        public uint TotalBattles;
        [JsonProperty("desertion")]
        public uint TotalDesertions;
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

        public int ExperienceNeed => RankExperience.GetExperienceFor(Rank);
        
        public byte Honor
        {
            get
            {
                if (TotalBattles == 0 && TotalDesertions == 0 && Assists == 0) return (byte) Enum.Honor.Average;
                return (byte)(Kills == 0 ? (byte)System.Math.Min(Kills, 4) : System.Math.Min(Deaths / Kills, 4));
            }
        }
    }
}