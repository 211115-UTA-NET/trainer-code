using RpsConsoleApp.UI.Dtos;

namespace RpsConsoleApp.UI
{
    public interface IGameService
    {
        Task<List<Round>> GetRoundsOfPlayerAsync(string name);
    }
}