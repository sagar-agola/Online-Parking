using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class Paging
    {
        [JsonProperty ("limit")]
        public int Limit { get; set; } = 20;

        [JsonProperty ("offset")]
        public int Offset { get; set; } = 1;
    }
}
