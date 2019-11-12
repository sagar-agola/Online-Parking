using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class Sorting
    {
        [JsonProperty ("orderBy")]
        public string OrderBy { get; set; } = "submitTimeUTC";

        [JsonProperty ("orderDescending")]
        public bool OrderDescending { get; set; } = true;
    }
}
