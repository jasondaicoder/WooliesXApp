using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WooliesX.Services
{
    /// <summary>
    /// Extension methods for <see cref="HttpClient"/>
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Get response and convert its content into object using json converter
        /// </summary>
        /// <param name="client">Instnace of <see cref="HttpClient"/></param>
        /// <param name="url">Url to request</param>
        /// <returns></returns>
        public static async Task<T> GetJsonAsync<T>(
            this HttpClient client,
            string url)
        {
            string content = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<T>(content);
        }
        
        /// <summary>
        /// Send a POST request with object as content to the url
        /// </summary>
        /// <param name="client">Instnace of <see cref="HttpClient"/></param>
        /// <param name="url">Url to request</param>
        /// <param name="contentObject">Object to send</param>
        /// <returns>Instance of <see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PatchAsJsonAsync(
            this HttpClient client,
            string url, 
            object contentObject)
        {
            using(StringContent content = new StringContent(GetJsonString(contentObject), Encoding.UTF8, "application/json"))
            {
                using(HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), url))
                {
                    request.Content = content;

                    return await client.SendAsync(request);
                }
            }
        }

        /// <summary>
        /// Send a PATCH request with object as content to the url
        /// </summary>
        /// <param name="client">Instnace of <see cref="HttpClient"/></param>
        /// <param name="url">Url to request</param>
        /// <param name="contentObject">Object to send</param>
        /// <returns>The return object</returns>
        public static async Task<T> PatchAsJsonAsync<T>(
            this HttpClient client,
            string url, 
            object contentObject)
        {
            using(HttpResponseMessage message = await client.PatchAsJsonAsync(url, contentObject))
            {
                message.EnsureSuccessStatusCode();

                return await message.Content.ReadAsJsonAsync<T>();
            }
        }
    
        /// <summary>
        /// Send a PUT request with object as content to the url
        /// </summary>
        /// <param name="client">Instnace of <see cref="HttpClient"/></param>
        /// <param name="url">Url to request</param>
        /// <param name="contentObject">Object to send</param>
        /// <returns>Instance of <see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PutAsJsonAsync(
            this HttpClient client,
            string url, 
            object contentObject)
        {
            using(StringContent content = new StringContent(GetJsonString(contentObject), Encoding.UTF8, "application/json"))
            {
                return await client.PutAsync(url, content);
            }
        }

        /// <summary>
        /// Send a PUT request with object as content to the url
        /// </summary>
        /// <param name="client">Instnace of <see cref="HttpClient"/></param>
        /// <param name="url">Url to request</param>
        /// <param name="contentObject">Object to send</param>
        /// <returns>Returned object</returns>
        public static async Task<T> PutAsJsonAsync<T>(
            this HttpClient client,
            string url, 
            object contentObject)
        {
            using(HttpResponseMessage message = await client.PutAsJsonAsync(url, contentObject))
            {
                message.EnsureSuccessStatusCode();

                return await message.Content.ReadAsJsonAsync<T>();
            }
        }

        /// <summary>
        /// Send a POST request with object as content to the url
        /// </summary>
        /// <param name="client">Instnace of <see cref="HttpClient"/></param>
        /// <param name="url">Url to request</param>
        /// <param name="contentObject">Object to send</param>
        /// <returns>Instance of <see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PostAsJsonAsync(
            this HttpClient client,
            string url, 
            object contentObject)
        {
            using(StringContent content = new StringContent(GetJsonString(contentObject), Encoding.UTF8, "application/json"))
            {
                return await client.PostAsync(url, content);
            }
        }

        /// <summary>
        /// Send a POST request with object as content to the url
        /// </summary>
        /// <param name="client">Instnace of <see cref="HttpClient"/></param>
        /// <param name="url">Url to request</param>
        /// <param name="contentObject">Object to send</param>
        /// <returns>Returned object</returns>
        public static async Task<T> PostAsJsonAsync<T>(
            this HttpClient client,
            string url, 
            object contentObject)
        {
            using(HttpResponseMessage message = await client.PostAsJsonAsync(url, contentObject))
            {
                message.EnsureSuccessStatusCode();

                return await message.Content.ReadAsJsonAsync<T>();
            }
        }

        /// <summary>
        /// Send a HEAD request to the url
        /// </summary>
        /// <param name="client">Instnace of <see cref="HttpClient"/></param>
        /// <param name="url">Url to request</param>
        /// <returns>Instance of <see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> HeadAsync(
            this HttpClient client,
            string url)
        {
            using(HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, url))
            {
                return await client.SendAsync(request);
            }
        }

        /// <summary>
        /// Send a DELETE request to the url
        /// </summary>
        /// <param name="client">Instnace of <see cref="HttpClient"/></param>
        /// <param name="url">Url to request</param>
        /// <returns>Returned object</returns>
        public static async Task<T> DeleteAsync<T>(
            this HttpClient client,
            string url)
        {
            using(HttpResponseMessage message = await client.DeleteAsync(url))
            {
                message.EnsureSuccessStatusCode();

                return await message.Content.ReadAsJsonAsync<T>();
            }
        }

        private static string GetJsonString(object contentObject)
        {
            return JsonConvert.SerializeObject(
                contentObject, 
                new JsonSerializerSettings 
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}