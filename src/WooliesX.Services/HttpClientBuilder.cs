using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WooliesX.Contracts;

namespace WooliesX.Services
{
    public static class HttpClientBuilder
    {
        public static HttpClient Build(AppSettings appSettings)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(appSettings.ResourceBaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}