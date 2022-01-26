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
            _logger.LogInformation($"database connection test: {(await _context.Players.FirstAsync()).Name}");
            throw new NotImplementedException();
        }

        public void AddNewRound(string? player1, string? player2, Logic.Round round)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PlayerExistsAsync(string player)
        {
            throw new NotImplementedException();
        }
    }
}
