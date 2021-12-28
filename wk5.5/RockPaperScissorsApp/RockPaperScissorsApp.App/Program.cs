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

using RockPaperScissorsApp.DataInfrastructure;
using RockPaperScissorsApp.Logic;

namespace RockPaperScissorsApp.App
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RockPaperScissors App");
            string? name = null;
            while (name == null || name.Length <= 0)
            {
                Console.Write("Enter an valid username: ");
                name = Console.ReadLine();
            }

            string connectionString = File.ReadAllText("C:/revature/richard-rps-db.txt");
            IRepository repository = new SqlRepository(connectionString);

            RandomMoveDecider moveDecider = new RandomMoveDecider();
            Game game = new(name, moveDecider, repository);
            Console.WriteLine($"[ Welcome Player {game.PlayerName}. ]");
            while (true)
            {
                Console.WriteLine();
                Console.Write("Play a round? (y/n) ");
                string? input = Console.ReadLine();
                if (input == null || input.ToLower() != "y")
                {
                    Console.WriteLine("--- End of the Game ---");
                    break;
                }
                game.PlayRound();
            }
            Console.WriteLine("--- Game Records ---");
            Console.WriteLine(game.Summary());
        }
    }
}
