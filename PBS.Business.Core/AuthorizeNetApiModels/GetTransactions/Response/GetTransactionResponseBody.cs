using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.Response;
using System.Collections.Generic;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactions.Response
{
    public class GetTransactionResponseBody
    {
        [JsonProperty ("messages")]
        public Messages Messages { get; set; }

        [JsonProperty ("transactions")]
        public List<Transaction> Transactions { get; set; }

        [JsonProperty ("totalNumInResultSet")]
        public int TotalNumInResultSet { get; set; }
    }
}
