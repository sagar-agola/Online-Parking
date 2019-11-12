using Newtonsoft.Json;
using PBS.Business.Core.AuthorizeNetApiModels.Response;
using System.Collections.Generic;

namespace PBS.Business.Core.AuthorizeNetApiModels.GetBatchList.Response
{
    public class GetBatchResponseBody
    {
        [JsonProperty ("batchList")]
        public List<BatchItem> BatchList { get; set; }

        [JsonProperty ("Messages")]
        public Messages Messages { get; set; }
    }
}
