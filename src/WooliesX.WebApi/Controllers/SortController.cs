using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WooliesX.Contracts;
using WooliesX.Services;

namespace WooliesX.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class SortController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IProductService _productService;

        public SortController(IOptions<AppSettings> appSettingsAccessor, IProductService productService)
        {
            _productService = productService;
            _appSettings = appSettingsAccessor.Value;
        }

        /// <summary>
        /// Sort product list by specified options
        /// </summary>
        /// <param name="sortOption">Sort option</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string sortOption)
        {
            if (string.IsNullOrEmpty(sortOption))
                return new BadRequestObjectResult("sortOption is not provided");

            SortOptions options;
            if (!SortOptions.TryParse(sortOption, out options))
                return new BadRequestObjectResult($"Invalid sortOption");

            var productList = await _productService.GetSortedProductsAsync(options);

            return Ok(productList);
        }
    }
}