namespace PBS.Business.Core.Models
{
    public class ResponseDetails
    {
        public ResponseDetails ()
        {
            Success = false;
            Data = null;
        }

        public ResponseDetails(bool success, object data)
        {
            Success = success;
            Data = data;
        }

        public bool Success { get; set; }

        public object Data { get; set; }
    }
}
