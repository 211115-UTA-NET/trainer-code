using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RpsApi.Api.Dtos;
using RpsApi.DataStorage;
using RpsApi.Logic;

namespace RpsApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<RoundsController> _logger;

        // asp.net better know how to handle making a controller
        // with some parameters like this, not just a zero-argument constructor
        // yes it can, it has a "dependency injector" aka DI container aka IServiceCollection
        //     configured at the top of Program.cs
        public RoundsController(IRepository repository, ILogger<RoundsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // in the "controller" part of the request-handling pipeline, there is a phase
        // called "model validation" right after model binding but before the action method runs.

        // model validation by default, only checks that non-nullable reference-type parameters did get a value.
        // add on more validation by using the right attributes. most of them from the DataAnnotations namespace

        // filters can be applied to individual action methods
        //        to whole controllers (= every action method within)
        //        to the whole app, using the right syntax in the Program.cs class

        // GET /api/rounds/?player=fred
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Round>>> GetAllByPlayerAsync([FromQuery, Required] string player)
        {
            //if (string.IsNullOrEmpty(player))
            //{
            //    return BadRequest("a player is required");
            //}
            // ^ not needed- model validation can handle it!
            // more complex validation
            IEnumerable<Round> rounds;
            try
            {
                rounds = await _repository.GetAllRoundsOfPlayerAsync(player);
            }
            catch (SqlException ex)
            {
                // bad! the exception is not logged, and asp.net was going to log it and return 500 anyway
                // (^ when the catch block just returned 500)
                // you only need to catch exceptions where either you want to do something besides a 500 error
                //     or you want to log the exception with some more context
                _logger.LogError(ex, "sql error while getting rounds of {player}", player);
                return StatusCode(500);
            }

            // asp.net already knows how to serialize json by default, using JsonResult
            // (which has a 200 status code on it)
            //return new JsonResult(rounds);

            // objectresult (and its derived classes) are good to use in a rest api,
            // because... they support content negotiation
            // objectresult will try to use whatever content type the request asked for
            // in the Accept header, if we have an outputformatter for it.

            // --- asp.net has a list of media types it can handle on input
            //   and on output, configured for the whole app in Program.cs
            //return new ObjectResult(rounds);

            // with the ActionResult<T> return type, returning "T" is automatically wrapped
            // in an ObjectResult (& 200 status code).
            return rounds.ToList();
        }

        // WIP
        // POST /api/rounds
        //[Consumes(MediaTypeNames.Application.Json)] // resource filter; rejects incoming requests with the wrong content type
        [HttpPost]
        public async Task<IActionResult> AddRoundAsync(SubmittedMove move)
        {
            // example of custom validation that attributes can't do
            // validate all user input, whether with attributes or not, whether it needs to check the database or not
            if (!await _repository.PlayerExistsAsync(move.Player1Name!))
            {
                // could use the ProblemDetails format like the auto validation messages, or just simple like this
                return BadRequest($"player {move.Player1Name} does not exist");
            }
            if (!await _repository.PlayerExistsAsync(move.Player2Name!))
            {
                return BadRequest($"player {move.Player2Name} does not exist");
            }

            // proceed with adding the move
            return StatusCode(500);
        }


        // GET /api/rounds/sample
        //[HttpGet("sample")]
        //public IActionResult AddSample([FromQuery, Required] int sample)
        //{
        //    return Ok(); // 200
        //}
    }
}
