using GamesAPI.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace GamesAPI.Controllers
{
    [Route("api/rawg")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class RawgController : ControllerBase
    {
        private readonly RawgClient _rawgClient;

        public RawgController(RawgClient rawgClient)
        {
            _rawgClient = rawgClient;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchGame([FromQuery] string name)
        {
            var result = await _rawgClient.GetGameDetailsAsync(name);
            return Ok(result);
        }
    }
}