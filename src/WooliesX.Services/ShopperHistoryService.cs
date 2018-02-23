using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WooliesX.Contracts;
using WooliesX.Contracts.Domain;

namespace WooliesX.Services
{
    public class ShopperHistoryService : IShopperHistoryService
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<ShopperHistoryService> _logger;

        public ShopperHistoryService(AppSettings appSettings, ILogger<ShopperHistoryService> logger)
        {
            _appSettings = appSettings;
            _logger = logger;
        }

        public async Task<List<ShopperHistory>> GetHistoriesAsync()
        {
            using(HttpClient httpClient = HttpClientBuilder.Build(_appSettings))
            {
                _logger.LogDebug($"Requesting products at {_appSettings.ResourceBaseUrl}/{Constants.Urls.ShopperHistory}");

                return await httpClient.GetJsonAsync<List<ShopperHistory>>($"{Constants.Urls.ShopperHistory}?token={_appSettings.Token}");
            }
        }
    }
}