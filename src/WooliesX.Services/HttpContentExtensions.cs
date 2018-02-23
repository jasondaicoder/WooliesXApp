using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WooliesX.Services
{
    /// <summary>
    /// Extension methods for <see cref="HttpContent"/>
    /// </summary>
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Read the content as object using json converter
        /// </summary>
        /// <param name="content">Instance of <see cref="HttpContent"/></param>
        /// <returns>Object converted using json converter</returns>
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string str = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}