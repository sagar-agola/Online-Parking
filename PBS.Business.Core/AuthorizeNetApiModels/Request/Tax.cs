using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class Tax
    {
        [JsonProperty ("amount")]
        public decimal Amount { get; set; }

        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("description")]
        public string Description { get; set; }
    }
}
