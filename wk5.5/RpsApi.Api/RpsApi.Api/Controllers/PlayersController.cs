using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RpsApi.DataStorage;

namespace RpsApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IRepository _repository;

        public PlayersController(IRepository repository)
        {
            _repository = repository;
        }

        // GET /api/players
        [HttpGet]
        public IActionResult GetAll()
        {
            return null;
            //_repository.
        }
    }
}
