using Microsoft.AspNetCore.Mvc;
using RpsApi.DataStorage;
using RpsApi.Logic;

namespace RpsApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundsController : ControllerBase
    {
        private readonly IRepository _repository;

        // asp.net better know how to handle making a controller
        // with some parameters like this, not just a zero-argument constructor
        // yes it can, it has a "dependency injector" aka DI container aka IServiceCollection
        //     configured at the top of Program.cs
        public RoundsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET /api/rounds/?player=fred
        [HttpGet]
        public async Task<IActionResult> GetAllByPlayerAsync([FromQuery] string player)
        {
            IEnumerable<Round> rounds = await _repository.GetAllRoundsOfPlayerAsync(player);

            // asp.net already knows how to serialize json by default, using JsonResult
            // (which has a 200 status code on it)
            return new JsonResult(rounds);
        }
    }
}
