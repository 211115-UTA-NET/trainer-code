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

            await PlayerExistsAsync(name);

            throw new NotImplementedException();
        }

        public void AddNewRound(string? player1, string? player2, Logic.Round round)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PlayerExistsAsync(string player)
        {
            //var x = player;
            //return await _context.Players.AnyAsync(p => p.Name == "nick') END; DROP TABLE Rps.Player;");
            return await _context.Players.AnyAsync(p => p.Name == player);
            // little micro-optimization: if an async method's only "await" is right at the return statement
            //     then you can skip the await and the method doesn't need to be async (return the Task directly)
        }
    }
}
