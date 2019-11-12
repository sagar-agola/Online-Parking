using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.GetTransactions.Response;
using PBS.Business.Core.AuthorizeNetApiModels.Response;
using System.Collections.Generic;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetUnsettledTransacions.Response
{
    public class GetUnsettledTransactionsResponseBody
    {
        [JsonProperty ("transactions")]
        public List<Transaction> Transactions { get; set; }

        [JsonProperty ("messages")]
        public Messages Messages { get; set; }
    }
}
