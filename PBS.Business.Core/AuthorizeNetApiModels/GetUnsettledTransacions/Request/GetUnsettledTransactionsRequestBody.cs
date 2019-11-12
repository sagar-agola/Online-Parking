using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetUnsettledTransacions.Request
{
    public class GetUnsettledTransactionsRequestBody
    {
        [JsonProperty ("getUnsettledTransactionListRequest")]
        public GetUnsettledTransactionListRequest GetUnsettledTransactionListRequest { get; set; }
    }
}
