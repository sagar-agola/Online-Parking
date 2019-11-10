using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class Payment
    {
        [JsonProperty ("creditCard")]
        public CreditCard CreditCard { get; set; }
    }
}
