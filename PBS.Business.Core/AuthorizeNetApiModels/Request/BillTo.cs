using Newtonsoft.Json;

namespace PBS.Business.Core.AuthorizeNetApiModels.Request
{
    public class BillTo
    {
        [JsonProperty ("firstName")]
        public string FirstName { get; set; }

        [JsonProperty ("lastName")]
        public string LastName { get; set; }

        [JsonProperty ("company")]
        public string Company { get; set; }

        [JsonProperty ("address")]
        public string Address { get; set; }

        [JsonProperty ("city")]
        public string City { get; set; }

        [JsonProperty ("state")]
        public string State { get; set; }

        [JsonProperty ("zip")]
        public string Zip { get; set; }

        [JsonProperty ("country")]
        public string Country { get; set; }
    }
}
