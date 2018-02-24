using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WooliesX.Contracts;
using WooliesX.Contracts.Domain;

namespace WooliesX.Services
{
    public class TrolleyService : ITrolleyService
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<TrolleyService> _logger;

        public TrolleyService(AppSettings appSettings,
            ILogger<TrolleyService> logger)
        {
            _appSettings = appSettings;
            _logger = logger;
        }

        public async Task<double> GetLowestTotal(Trolley trolley)
        {
            using(HttpClient httpClient = HttpClientBuilder.Build(_appSettings))
            {
                _logger.LogDebug($"Requesting trolley calculator service at {_appSettings.ResourceBaseUrl}/{Constants.Urls.Products}");


                using(HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                    $"{Constants.Urls.TrolleyCalculator}?token={_appSettings.Token}",
                    trolley))
                {
                    string content = await response.Content.ReadAsStringAsync();

                    try
                    {
                        response.EnsureSuccessStatusCode();

                        double total = 0;
                        double.TryParse(content, out total);

                        return total;
                    }
                    catch(HttpRequestException)
                    {
                        _logger.LogError("Error occurred while requesting trolley calculate service, error message: " + content);

                        throw;
                    }
                }
            }
        }
    }
}