using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace WooliesX.Services.Tests
{
    public class ShopperHistoryServiceTests : TestBase
    {
        [Fact]
        public async Task GetHistories()
        {
            Mock<ILogger<ShopperHistoryService>> loggerMock = new Mock<ILogger<ShopperHistoryService>>();

            ShopperHistoryService service = new ShopperHistoryService(GetAppSettings(), loggerMock.Object);

            var histories = await service.GetHistoriesAsync();

            Assert.NotNull(histories);
        }
    }
}