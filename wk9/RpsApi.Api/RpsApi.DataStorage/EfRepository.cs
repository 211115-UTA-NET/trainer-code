using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RpsApi.DataStorage.Model;
using RpsApi.Logic;

namespace RpsApi.DataStorage
{
    public class EfRepository : IRepository
    {
        private readonly RPSContext _context;
        private readonly ILogger<EfRepository> _logger;

        public EfRepository(RPSContext context, ILogger<EfRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Logic.Round>> GetAllRoundsOfPlayerAsync(string name)
        {
            //_logger.LogInformation($"database connection test: {(await _context.Players.FirstAsync()).Name}");
            //await PlayerExistsAsync(name);

            List<Model.Round> rounds = await _context.Rounds
                .Where(r => r.Player1Navigation != null && r.Player1Navigation.Name == name)
                .ToListAsync();

            var neededMoveIds = rounds.SelectMany(r => new[] { r.Player1Move, r.Player2Move });
            var neededMoves = _context.Moves.Where(m => neededMoveIds.Contains(m.Id));

            // need to map from the EF model to the classes the consumers of this repository expect (Logic.Round)
            // (otherwise we could just do "return rounds;")
            return rounds.Select(r =>
            {
                // here, i have a string like "rock" and i want an enum value like Logic.Move.Rock
                var m1 = (Logic.Move)Enum.Parse(typeof(Logic.Move), neededMoves.First(m => m.Id == r.Player1Move).Name);
                var m2 = (Logic.Move)Enum.Parse(typeof(Logic.Move), neededMoves.First(m => m.Id == r.Player2Move).Name);
                return new Logic.Round(r.Timestamp, m1, m2);
            });
        }

        public void AddNewRound(string? player1, string? player2, Logic.Round round)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PlayerExistsAsync(string player)
        {
            //var x = player;
            //return await _context.Players.AnyAsync(p => p.Name == "nick') END; DROP TABLE Rps.Player;");

            // EF automatically SQL-escapes any strings involved, preventing SQL injection.
            // it also uses SqlParameters for any variables used in the query, further preventing SQL injection
            return await _context.Players.AnyAsync(p => p.Name == player);
            // little micro-optimization: if an async method's only "await" is right at the return statement
            //     then you can skip the await and the method doesn't need to be async (return the Task directly)
        }
    }
}
