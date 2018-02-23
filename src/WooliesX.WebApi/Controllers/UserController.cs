using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WooliesX.Contracts;

namespace WooliesX.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppSettings _appSettings;

        public UserController(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public IActionResult Get()
        {
            return Ok(new {
                Name = "Jason Dai",
                Token = _appSettings.Token
            });
        }
    }
}