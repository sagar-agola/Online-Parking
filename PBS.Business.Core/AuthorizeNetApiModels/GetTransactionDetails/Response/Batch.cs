using Newtonsoft.Json;
using System;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactionDetails.Response
{
    public class Batch
    {
        [JsonProperty ("batchId")]
        public string BatchId { get; set; }

        [JsonProperty ("settlementTimeUTC")]
        public DateTime SettlementTimeUTC { get; set; }

        [JsonProperty ("settlementTimeLocal")]
        public DateTime SettlementTimeLocal { get; set; }

        [JsonProperty ("settlementState")]
        public string SettlementState { get; set; }
    }
}
