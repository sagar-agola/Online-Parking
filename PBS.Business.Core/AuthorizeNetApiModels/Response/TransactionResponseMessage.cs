using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Response
{
    public class TransactionResponseMessage
    {
        [JsonProperty ("code")]
        public string Code { get; set; }

        [JsonProperty ("description")]
        public string Description { get; set; }
    }
}
