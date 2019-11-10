using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class TransactionRequest
    {
        [JsonProperty ("transactionType")]
        public string TransactionType { get; set; } = "authCaptureTransaction";

        [JsonProperty ("amount")]
        public decimal Amount { get; set; }

        [JsonProperty ("payment")]
        public Payment Payment { get; set; }

        [JsonProperty ("tax")]
        public Tax Tax { get; set; }

        [JsonProperty ("customer")]
        public Customer Customer { get; set; }

        [JsonProperty ("billTo")]
        public BillTo BillTo { get; set; }
    }
}
