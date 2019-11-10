using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class MerchantAuthentication
    {
        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("transactionKey")]
        public string TransactionKey { get; set; }
    }
}
