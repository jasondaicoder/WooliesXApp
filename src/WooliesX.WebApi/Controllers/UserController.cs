using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WooliesX.Contracts;

namespace WooliesX.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppSettings appSettings;

        public UserController(IOptions<AppSettings> appSettingsAccessor)
        {
            appSettings = appSettingsAccessor.Value;
        }

        public IActionResult Get()
        {
            return Ok(new {
                Name = "Jason",
                Token = appSettings.Token
            });
        }
    }
}