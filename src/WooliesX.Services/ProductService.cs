using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WooliesX.Contracts;
using WooliesX.Contracts.Domain;

namespace WooliesX.Services
{
    public class ProductService : IProductService
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<ProductService> _logger;
        private readonly IShopperHistoryService _shopperHistoryService;

        public ProductService(
            AppSettings appSettings,
            ILogger<ProductService> logger,
            IShopperHistoryService shopperHistoryService)
        {
            _appSettings = appSettings;
            _logger = logger;
            _shopperHistoryService = shopperHistoryService;
        }

        public async Task<List<Product>> GetSortedProductsAsync(SortOptions sortOptions)
        {
            using(HttpClient httpClient = HttpClientBuilder.Build(_appSettings))
            {
                _logger.LogDebug($"Requesting products at {_appSettings.ResourceBaseUrl}/{Constants.Urls.Products}");

                List<Product> products = await httpClient.GetJsonAsync<List<Product>>($"{Constants.Urls.Products}?token={_appSettings.Token}");

                if (products != null && products.Any())
                {
                    switch (sortOptions)
                    {
                        case SortOptions.High:
                            return products.OrderByDescending(p => p.Price).ToList();

                        case SortOptions.Low:
                            return products.OrderBy(p => p.Price).ToList();

                        case SortOptions.Ascending:
                            return products.OrderBy(p => p.Name).ToList();

                        case SortOptions.Descending:
                            return products.OrderByDescending(p => p.Name).ToList();

                        case SortOptions.Recommended:
                        default:
                            Dictionary<string, int> popularity = await GetProductPopularityAsync();
                            if (popularity != null)
                            {
                                return products.OrderByDescending(p => GetPopularity(popularity, p.Name)).ToList();
                            }
                            return products;
                    }
                }

                return null;
            }
        }

        private async Task<Dictionary<string, int>> GetProductPopularityAsync()
        {
            var histories = await _shopperHistoryService.GetHistoriesAsync();

            Dictionary<string, int> popularity = new Dictionary<string, int>();
            foreach(ShopperHistory history in histories)
            {
                foreach(Product product in history.Products)
                {
                    if (!popularity.ContainsKey(product.Name))
                    {
                        popularity.Add(product.Name, 1);
                    }
                    else
                    {
                        popularity[product.Name]++;
                    }
                }
            }

            return popularity;
        }

        private int GetPopularity(Dictionary<string, int> popularities, string name)
        {
            if (popularities.ContainsKey(name))
                return popularities[name];
            else
                return 0;
        }
    }
}
