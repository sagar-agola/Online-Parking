using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Response
{
    public class ApiResponseBody
    {
        [JsonProperty ("transactionResponse")]
        public TransactionResponse TransactionResponse { get; set; }

        [JsonProperty ("refId")]
        public string RefId { get; set; }

        [JsonProperty ("messages")]
        public Messages Messages { get; set; }
    }
}
