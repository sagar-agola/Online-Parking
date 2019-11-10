using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Response
{
    public class Message
    {
        [JsonProperty ("code")]
        public string Code { get; set; }

        [JsonProperty ("text")]
        public string Text { get; set; }
    }
}
