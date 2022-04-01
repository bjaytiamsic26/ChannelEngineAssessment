using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Helper
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void Initialize()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(Constants.BaseUri);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static string UrlBuilder(string apiExt)
        {
            return $"{ApiClient.BaseAddress.ToString()}{apiExt}{Constants.ApiKeyUri}";
        }
    }
}
