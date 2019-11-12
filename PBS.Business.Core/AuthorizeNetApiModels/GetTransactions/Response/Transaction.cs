using Newtonsoft.Json;
using System;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetTransactions.Response
{
    public class Transaction
    {
        [JsonProperty ("transId")]
        public string TransId { get; set; }

        [JsonIgnore]
        public string EncryptedTransactionId { get; set; }

        [JsonProperty ("submitTimeUTC")]
        public DateTime SubmitTimeUTC { get; set; }

        [JsonProperty ("submitTimeLocal")]
        public DateTime SubmitTimeLocal { get; set; }

        [JsonProperty ("transactionStatus")]
        public string TransactionStatus { get; set; }

        [JsonProperty ("firstName")]
        public string FirstName { get; set; }

        [JsonProperty ("lastName")]
        public string LastName { get; set; }

        [JsonProperty ("settleAmount")]
        public decimal Amount { get; set; }

        [JsonProperty ("accountType")]
        public string AccountType { get; set; }

        [JsonProperty ("accountNumber")]
        public string AccountNumber { get; set; }
    }
}