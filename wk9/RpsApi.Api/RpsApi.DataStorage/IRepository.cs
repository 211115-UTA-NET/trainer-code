using RpsApi.Logic;

namespace RpsApi.DataStorage
{
    public interface IRepository
    {
        Task<IEnumerable<string>> GetAllPlayersAsync();
        Task<IEnumerable<Round>> GetAllRoundsOfPlayerAsync(string name);
        Task AddNewRoundAsync(string player1, string? player2, Round round);

        Task<bool> PlayerExistsAsync(string player);
        Task AddPlayerAsync(string player);

        //void Save();
    }
}
