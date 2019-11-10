using Newtonsoft.Json;
using System.Collections.Generic;

namespace PBS.Business.Core.AuthorizeNetApiModels.Response
{
    public class TransactionResponse
    {
        [JsonProperty ("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty ("transId")]
        public string TransId { get; set; }

        [JsonProperty ("transHash")]
        public string TransHash { get; set; }

        [JsonProperty ("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty ("accountType")]
        public string AccountType { get; set; }

        [JsonProperty ("messages")]
        public List<TransactionResponseMessage> Messages { get; set; }

        [JsonProperty ("errors")]
        public List<Error> Errors { get; set; }
    }
}
