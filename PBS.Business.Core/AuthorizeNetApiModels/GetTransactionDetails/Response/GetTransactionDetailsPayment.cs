using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactionDetails.Response
{
    public class GetTransactionDetailsPayment
    {
        [JsonProperty ("creditCard")]
        public GetTransactionDetailsCreditCard CreditCard { get; set; }
    }
}