using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PBS.Business.Core.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PBS.Web.Helpers
{
    public class ApiHelper : IApiHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenDecoder _tokenDecoder;

        public ApiHelper (IConfiguration configuration, ITokenDecoder tokenDecoder)
        {
            _configuration = configuration;
            _tokenDecoder = tokenDecoder;
        }

        /// <summary>
        /// Common helper method to send API request
        /// </summary>
        /// <typeparam name="T">Generic type of data</typeparam>
        /// <param name="data">data to send over API request</param>
        /// <param name="url">Relative URL of API</param>
        /// <param name="httpMethod">Method of API request</param>
        /// <returns></returns>
        public ResponseDetails SendApiRequest<T> (T data, string url, HttpMethod httpMethod)
        {
            ResponseDetails responseModel = new ResponseDetails ();

            try
            {
                string baseUrl = _configuration.GetSection ("APIUrl").Value;
                baseUrl += url;

                if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Delete)
                {
                    baseUrl += Convert.ToString (data);
                }

                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri (baseUrl)
                };

                client.DefaultRequestHeaders.Accept.Clear ();
                client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

                if (_tokenDecoder.IsLoggedIn)
                {
                    client.DefaultRequestHeaders.Add ("Authorization", "Bearer " + _tokenDecoder.RowToken);
                }

                HttpResponseMessage response;

                if (httpMethod == HttpMethod.Post)
                {
                    response = client.PostAsJsonAsync (baseUrl, data).Result;
                }
                else if (httpMethod == HttpMethod.Delete)
                {
                    response = client.DeleteAsync (baseUrl).Result;
                }
                else
                {
                    response = client.GetAsync (baseUrl).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var result = content.ReadAsStringAsync ().Result;

                    dynamic returnObj = JObject.Parse (result);

                    if (returnObj != null)
                    {
                        if (returnObj["data"] != null)
                        {
                            responseModel.Data = returnObj["data"];
                        }
                        if (returnObj["success"] != null)
                        {
                            responseModel.Success = returnObj["success"];
                        }
                    }
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Data = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Data = ex.Message;

                return responseModel;
            }

            return responseModel;
        }
    }
}