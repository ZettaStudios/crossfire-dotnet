using Newtonsoft.Json;

namespace Login.Model
{
    public class VerifyResult
    {
        [JsonProperty("result")] public bool Result;
    }
}