/*
 * play RPS with AI/Computer
 * required features:
 * - play multiple rounds
 * - get a summary/record of all the rounds played so far
 *
 * stretch goal features:
 * - persistence (save data somehow, it will remember past game history)
 * - play more complex variants of RPS (like RPS+lizard+spock)
 * - logging (to help with debugging the app if something goes wrong)
 * - more than 2 players
 * - support both player vs player and player vs computer
 * - difficulty settings for the computer (remembers your moves and tries to predict)
 * - timmer limit
 *
 * - In general, we want to write something simple
 *   but in a way that allows for extending it/generalizing it in the future.
 */

using System.Net.Http.Json;
using System.Text.Json;
using RpsConsoleApp.UI;
using RpsConsoleApp.UI.Dtos;
using RpsConsoleApp.UI.Exceptions;

namespace RockPaperScissorsApp.App
{
    public class Program
    {
        public static readonly HttpClient HttpClient = new();

        public async static Task Main(string[] args)
        {
            Console.WriteLine("Welcome to RockPaperScissors App");
            string? name = null;
            while (name == null || name.Length <= 0)
            {
                Console.Write("Enter an valid username: ");
                name = Console.ReadLine();
            }

            // not great to hardcode the URL, but if you do, best that it's up in the main method
            // and not buried somewhere not obvious, or worse,
            // duplicated in several places.
            // better approach would be, read it from command line args, environment variables, or maybe a file
            Uri server = new("https://localhost:7175");

            IGameService gameService = new GameService(server);

            // Accept header on the request tells the server what media type you expect in the response to that request.
            //HttpResponseMessage response = await HttpClient.GetAsync($"https://localhost:7175/api/rounds?player={name}");
            List<Round> rounds;
            try
            {
                rounds = await gameService.GetRoundsOfPlayerAsync(name);
            }
            catch (UnexpectedServerBehaviorException)
            {
                Console.WriteLine("Fatal error, can't properly connect to server.");
                return;
            }

            // after you await the task, you have the status code & headers of the response, but not yet the response body
            //response.Content.Headers.ContentType == "application/json";
            //string json = await response.Content.ReadAsStringAsync();
            //var rounds = JsonSerializer.Deserialize<List<Round>>(json);

            Console.WriteLine(Game.Summary(name, rounds));

            //RandomMoveDecider moveDecider = new RandomMoveDecider();
            //Game game = new(name, moveDecider, repository);
            //Console.WriteLine($"[ Welcome Player {game.PlayerName}. ]");
            //while (true)
            //{
            //    Console.WriteLine();
            //    Console.Write("Play a round? (y/n) ");
            //    string? input = Console.ReadLine();
            //    if (input == null || input.ToLower() != "y")
            //    {
            //        Console.WriteLine("--- End of the Game ---");
            //        break;
            //    }
            //    game.PlayRound();
            //}
            //Console.WriteLine("--- Game Records ---");
            //// synchronously wait on the Task (can't do anything else in the meantime)
            //// bad! unless it's the Main method, where clearly there's no "other code" that could be running
            ////Console.WriteLine(game.SummaryAsync().Result);
            //Console.WriteLine(await game.SummaryAsync());
        }
    }
}
