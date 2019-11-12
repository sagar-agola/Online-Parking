using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.Response;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactionDetails.Response
{
    public class GetTransactionDetailsResponseBody
    {
        [JsonProperty ("messages")]
        public Messages Messages { get; set; }

        [JsonProperty ("transaction")]
        public GetTransactionDetailsTransaction Transaction { get; set; }
    }
}
