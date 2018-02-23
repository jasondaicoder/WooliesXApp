using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WooliesX.Contracts;
using WooliesX.Services;

namespace WooliesX.WebApi.Controllers
{
    public class SortController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IProductService _productService;

        public SortController(IOptions<AppSettings> appSettingsAccessor, IProductService productService)
        {
            _productService = productService;
            _appSettings = appSettingsAccessor.Value;
        }

        public async Task<IActionResult> Get(string sortOptions)
        {
            if (string.IsNullOrEmpty(sortOptions))
                return new BadRequestObjectResult("Sort options is not provided");

            SortOptions options;
            if (!SortOptions.TryParse(sortOptions, out options))
                return new BadRequestObjectResult($"Invalid sort options");

            var productList = await _productService.GetSortedProductsAsync(options);

            return Ok(productList);
        }
    }
}