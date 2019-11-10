using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class ApiRequestBody
    {
        [JsonProperty ("createTransactionRequest")]
        public CreateTransactionRequest CreateTransactionRequest { get; set; }
    }
}
