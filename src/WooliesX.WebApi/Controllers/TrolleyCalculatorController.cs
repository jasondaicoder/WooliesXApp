using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WooliesX.Contracts.Domain;
using WooliesX.Services;
using WooliesX.WebApi.Models;

namespace WooliesX.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TrolleyCalculatorController : Controller
    {
        private readonly ITrolleyService _trolleyService;
        public TrolleyCalculatorController(ITrolleyService trolleyService)
        {
            _trolleyService = trolleyService;
        }

        /// <summary>
        /// Get lowest total of the trolley items
        /// </summary>
        /// <param name="trolley">Instance of trolley</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Trolley trolley)
        {
            if (trolley == null)
                return new BadRequestObjectResult("trolley is not provided");

            double total = await _trolleyService.GetLowestTotal(trolley);

            return Ok(total);
        }
    }
}