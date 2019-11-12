using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.Request;
using System;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactionDetails.Response
{
    public class GetTransactionDetailsTransaction
    {
        [JsonProperty ("transId")]
        public string TransId { get; set; }

        [JsonIgnore]
        public string EncryptedTransactionId { get; set; }

        [JsonProperty ("submitTimeUTC")]
        public DateTime SubmitTimeUTC { get; set; }

        [JsonProperty ("submitTimeLocal")]
        public DateTime SubmitTimeLocal { get; set; }

        [JsonProperty ("transactionType")]
        public string TransactionType { get; set; }

        [JsonProperty ("transactionStatus")]
        public string TransactionStatus { get; set; }

        [JsonProperty ("responseReasonDescription")]
        public string ResponseReason { get; set; }

        [JsonProperty ("requestedAmount")]
        public decimal RequestedAmount { get; set; }

        [JsonProperty ("authAmount")]
        public decimal AuthAmount { get; set; }

        [JsonProperty ("batch")]
        public Batch Batch { get; set; }

        [JsonProperty ("tax")]
        public Tax Tax { get; set; }

        [JsonProperty ("payment")]
        public GetTransactionDetailsPayment Payment { get; set; }

        [JsonProperty ("customer")]
        public Customer Customer { get; set; }

        [JsonProperty ("billTo")]
        public BillTo BillTo { get; set; }
    }
}
