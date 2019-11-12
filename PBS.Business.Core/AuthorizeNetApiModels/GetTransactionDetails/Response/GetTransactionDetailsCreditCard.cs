using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactionDetails.Response
{
    public class GetTransactionDetailsCreditCard
    {
        [JsonProperty ("cardNumber")]
        public string CardNumber { get; set; }

        [JsonProperty ("cardType")]
        public string CardType { get; set; }
    }
}