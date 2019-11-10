using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class CreateTransactionRequest
    {
        [JsonProperty ("merchantAuthentication")]
        public MerchantAuthentication MerchantAuthentication { get; set; }

        [JsonProperty ("refId")]
        public string RefId { get; set; }

        [JsonProperty ("transactionRequest")]
        public TransactionRequest TransactionRequest { get; set; }
    }
}
