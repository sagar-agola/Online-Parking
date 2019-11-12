using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.Request;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactionDetails.Request
{
    public class GetTransactionDetailsRequest
    {
        [JsonProperty ("merchantAuthentication")]
        public MerchantAuthentication MerchantAuthentication { get; set; }

        [JsonProperty ("transId")]
        public string TransId { get; set; }
    }
}
