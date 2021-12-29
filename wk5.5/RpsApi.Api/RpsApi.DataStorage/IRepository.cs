using RpsApi.Logic;

namespace RpsApi.DataStorage
{
    public interface IRepository
    {
        Task<IEnumerable<Round>> GetAllRoundsOfPlayerAsync(string name);
        void AddNewRound(string? player1, string? player2, Round round);

        Task<bool> PlayerExistsAsync(string player);

        //void Save();
    }
}
