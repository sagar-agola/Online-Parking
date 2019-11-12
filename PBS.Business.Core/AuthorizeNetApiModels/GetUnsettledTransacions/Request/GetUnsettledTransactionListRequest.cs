using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.Request;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetUnsettledTransacions.Request
{
    public class GetUnsettledTransactionListRequest
    {
        [JsonProperty ("merchantAuthentication")]
        public MerchantAuthentication MerchantAuthentication { get; set; }

        [JsonProperty ("sorting")]
        public Sorting Sorting { get; set; }

        [JsonProperty ("paging")]
        public Paging Paging { get; set; }
    }
}