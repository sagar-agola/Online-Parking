using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class CreditCard
    {
        [JsonProperty ("cardNumber")]
        public string CardNumber { get; set; }

        [JsonProperty ("expirationDate")]
        public string ExpirationDate { get; set; }

        [JsonProperty ("cardCode")]
        public string CardCode { get; set; }
    }
}
