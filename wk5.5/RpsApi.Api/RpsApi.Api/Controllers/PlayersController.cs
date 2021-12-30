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

        // logging is important
        // you can use the debugger for problems that you can identify & reproduce on our machine.
        // but you can't use the debugger on the user's machine or on the real production server
        //   you have to rely on logs
        // some logs are not structured - just text being written to a file.
        //    want to find something? ctrl+f is all you've got
        // often logs are "structured" to some extent
        //    have a consistent format with a lot of metadata to help software query/filter those logs.
        // also, usually we have "log levels"
        //    sometimes you only care about big errors...
        // other times you want to track down all the details of some operation
        // in .NET - Trace = 0, Debug = 1, Information = 2, Warning = 3, Error = 4, Critical = 5

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
