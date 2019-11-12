using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetBatchList.Request
{
    public class GetBatchRequestBody
    {
        [JsonProperty ("getSettledBatchListRequest")]
        public GetSettledBatchListRequest GetGetSettledBatchListRequest { get; set; }
    }
}
