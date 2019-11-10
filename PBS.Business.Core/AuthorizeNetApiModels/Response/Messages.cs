using Newtonsoft.Json;
using System.Collections.Generic;

namespace PBS.Business.Core.AuthorizeNetApiModels.Response
{
    public class Messages
    {
        [JsonProperty ("resultCode")]
        public string ResultCode { get; set; }

        [JsonProperty ("message")]
        public List<Message> Message { get; set; }
    }
}
