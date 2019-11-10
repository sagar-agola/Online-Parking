using System.Net.Http;
using PBS.Business.Core.Models;

namespace PBS.Web.Helpers
{
    public interface IApiHelper
    {
        ResponseDetails SendApiRequest<T> (T data, string url, HttpMethod httpMethod);
        ResponseDetails SendPaymentApiRequest (Business.Core.AuthorizeNetApiModels.Request.ApiRequestBody requestBody);
    }
}