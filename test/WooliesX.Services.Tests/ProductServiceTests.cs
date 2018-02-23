using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using WooliesX.Contracts;
using WooliesX.Contracts.Domain;
using Xunit;

namespace WooliesX.Services.Tests
{
    public class ProductServiceTests : TestBase
    {
        [Fact]
        public async Task TestSortProducts()
        {
            Mock<ILogger<ProductService>> loggerMock = new Mock<ILogger<ProductService>>();
            Mock<IShopperHistoryService> historyServiceMock = new Mock<IShopperHistoryService>();
            historyServiceMock
                .Setup(m => m.GetHistoriesAsync())
                .ReturnsAsync(new List<ShopperHistory>
                {
                    new ShopperHistory
                    {
                        CustomerId = 1,
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Name = "Test Product A",
                                Price = 100,
                                Quantity = 100
                            }
                        }
                    },
                });

            ProductService service = new ProductService(GetAppSettings(), loggerMock.Object, historyServiceMock.Object);

            var products = await service.GetSortedProductsAsync(SortOptions.Recommended);

            Assert.NotNull(products);
        }
    }
}
