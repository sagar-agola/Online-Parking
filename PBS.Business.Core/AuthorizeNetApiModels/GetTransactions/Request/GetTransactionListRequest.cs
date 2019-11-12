using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.Request;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactions.Request
{
    public class GetTransactionListRequest
    {
        [JsonProperty ("merchantAuthentication")]
        public MerchantAuthentication MerchantAuthentication { get; set; }

        [JsonProperty ("batchId")]
        public string BatchId { get; set; }

        [JsonProperty ("sorting")]
        public Sorting Sorting { get; set; }

        [JsonProperty ("paging")]
        public Paging Paging { get; set; }
    }
}
