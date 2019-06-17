using Newtonsoft.Json;

namespace Apple_Accounts_Creator.Register
{
    public class Verifycation
    {
        [JsonProperty("verificationId")]
        public string Id { get; set; }
        [JsonProperty("canGenerateNew")]
        public bool CanGenerateNew { get; set; }
        [JsonProperty("length")]
        public int Length { get; set; }
    }
}
