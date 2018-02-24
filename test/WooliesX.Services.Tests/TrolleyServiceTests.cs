using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using WooliesX.Contracts.Domain;
using Xunit;

namespace WooliesX.Services.Tests
{
    public class TrolleyServiceTests : TestBase
    {
        [Fact]
        public async Task GetLowestTotal()
        {
            Trolley trolley = new Trolley
            {
                Products = new List<ProductInfo>
                {
                    new ProductInfo
                    {
                        Name = "test1",
                        Price = 10
                    }
                },
                Specials = new List<SpecialItem>
                {
                    new SpecialItem
                    {
                        Quantities = new List<ProductItem>
                        {
                            new ProductItem
                            {
                                Name = "test1",
                                Quantity = 2
                            }
                        },
                        Total = 2
                    }
                },
                Quantities = new List<ProductItem>
                {
                    new ProductItem
                    {
                        Name = "test1",
                        Quantity = 3
                    }
                }
            };
            
            Mock<ILogger<TrolleyService>> loggerMock = new Mock<ILogger<TrolleyService>>();
            TrolleyService service = new TrolleyService(GetAppSettings(), loggerMock.Object);

            double total = await service.GetLowestTotal(trolley);

            Assert.Equal(12.0, total);
        }
    }
}