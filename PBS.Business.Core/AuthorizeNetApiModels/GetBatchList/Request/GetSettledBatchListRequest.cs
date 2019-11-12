using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.Request;
using System;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetBatchList.Request
{
    public class GetSettledBatchListRequest
    {
        [JsonProperty ("merchantAuthentication")]
        public MerchantAuthentication MerchantAuthentication { get; set; }

        [JsonProperty ("firstSettlementDate")]
        public DateTime FirstSettlementDate { get; set; }

        [JsonProperty ("lastSettlementDate")]
        public DateTime lastSettlementDate { get; set; }
    }
}
