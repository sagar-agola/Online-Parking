using Newtonsoft.Json;
using System;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetBatchList.Response
{
    public class BatchItem
    {
        [JsonProperty ("batchId")]
        public string BatchId { get; set; }

        [JsonIgnore]
        public string EncryptedBatchId { get; set; }

        [JsonProperty ("settlementTimeUTC")]
        public DateTime SettlementTimeUTC { get; set; }

        [JsonProperty ("paymentMethod")]
        public string PaymentMethod { get; set; }
    }
}
