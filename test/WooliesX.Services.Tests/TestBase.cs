using System.IO;
using Microsoft.Extensions.Configuration;
using WooliesX.Contracts;

namespace WooliesX.Services.Tests
{
    public class TestBase
    {
        protected IConfigurationSection GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Microsoft.Extensions.Configuration.IConfiguration configuration = builder.Build();

            return configuration.GetSection("AppSettings");
        }

        protected AppSettings GetAppSettings()
        {
            return new AppSettings
            {
                ResourceBaseUrl = GetConfiguration()["ResourceBaseUrl"],
                Token = GetConfiguration()["Token"]
            };
        }
    }
}