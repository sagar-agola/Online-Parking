using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactionDetails.Request
{
    public class GetTransactionDetailsRequestBody
    {
        [JsonProperty ("getTransactionDetailsRequest")]
        public GetTransactionDetailsRequest GetTransactionDetailsRequest { get; set; }
    }
}
