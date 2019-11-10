using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Response
{
    public class Error
    {
        [JsonProperty ("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty ("errorText")]
        public string ErrorText { get; set; }
    }
}
