using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetBatchList.Response
{
    public class BatchItem
    {
        [JsonProperty ("batchId")]
        public string BatchId { get; set; }

        [JsonProperty ("settlementState")]
        public string SettlementState { get; set; }

        [JsonProperty ("paymentMethod")]
        public string PaymentMethod { get; set; }
    }
}
