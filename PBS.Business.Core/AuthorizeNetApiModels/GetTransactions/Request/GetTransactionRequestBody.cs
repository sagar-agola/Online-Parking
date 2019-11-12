using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactions.Request
{
    public class GetTransactionRequestBody
    {
        [JsonProperty ("getTransactionListRequest")]
        public GetTransactionListRequest GetTransactionListRequest { get; set; }
    }
}
