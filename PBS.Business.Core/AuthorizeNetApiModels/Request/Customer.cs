using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class Customer
    {
        [JsonProperty ("type")]
        public string Type { get; set; } = "individual";

        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonIgnore]
        public string EncryptedId { get; set; }

        [JsonProperty ("email")]
        public string Email { get; set; }
    }
}
