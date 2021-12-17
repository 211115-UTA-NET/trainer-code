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

using System.Diagnostics;
using System.Xml.Serialization;
using RockPaperScissorsApp.Logic;

namespace RockPaperScissorsApp.App
{
    public class Program
    {
        // separate parts of the code that have their own distinct purposes
        // from each other

        // five design principles: SOLID
        // S: single responsibility principle
        //   each class should have "one responsibility" (not several)
        //   each class should have "one reason to change"
        // we can apply the same idea at many scales: variable, method, project, apps/deployments

        public static void Main(string[] args)
        {
            //CastingDemo.NumericCasting();
            CastingDemo.NonNumericCasting();
            Console.WriteLine("Welcome to RockPaperScissors App");
            string? name = null;
            while (name == null || name.Length <= 0)
            {
                Console.Write("Enter an valid username: ");
                Thread.Sleep(1000);
                //GC.Collect();
                name = Console.ReadLine();
            }
            List<Round>? records = ReadHistoryFromFile("../../../history.xml");
            IMoveDecider moveDecider = new RandomMoveDecider();
            Game game = new(name, moveDecider, records);
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
            game.Summary();
            WriteHistoryToFile(game, "../../../history.xml");
        }

        private static void WriteHistoryToFile(Game game, string filePath)
        {
            string xml = game.SerializeAsXml();
            File.WriteAllText(filePath, xml);
        }

        // any objects inside the CLR are automatically cleaned up by the garbage collector
        // when we're done with them.
        // but... some objects contain/handle resources outside the CLR (e.g. file system, network).
        //  e.g. StreamReader opening a file.
        // for those, you have to explicitly call the Close or Dispose method.
        // typically you do this in a finally block to be 100% sure that it will be called in all cases.
        // .NET has a special interface called IDisposable that basically tells you
        //     this is a class that needs to be Disposed when you're done.

        private static List<Round>? ReadHistoryFromFileOld(string filePath)
        {
            XmlSerializer serializer = new(typeof(List<Round>));
            StreamReader? reader = null;
            try
            {
                reader = new(filePath);
                var records = (List<Round>?)serializer.Deserialize(reader);
                return records;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        private static List<Round>? ReadHistoryFromFile(string filePath)
        {
            XmlSerializer serializer = new(typeof(List<Serialization.Record>));

            try
            {
                // using statement can be a block, or just one line
                // if it's one line, the dispose happens whenever the variable
                // goes out of scope
                using StreamReader reader = new(filePath);

                var records = (List<Serialization.Record>?)serializer.Deserialize(reader);
                if (records is null) throw new InvalidDataException();

                // sneak peak into nice advanced feature called LINQ, using lambda expression delegates
                return records.Select(x => x.CreateRound()).ToList();
            }

            catch (IOException)
            {
                return null;
            }
        }
    }
}
