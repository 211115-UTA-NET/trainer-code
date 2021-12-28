using System.Text;
using RockPaperScissorsApp.DataInfrastructure;
using RockPaperScissorsApp.Logic;

namespace RockPaperScissorsApp.App
{
    public class Game
    {
        public string PlayerName { get; }
        private readonly IMoveDecider cpuMoveDecider;
        private readonly IRepository _repository;

        // constructor
        public Game(string playerName, IMoveDecider cpuMoveDecider, IRepository repository)
        {
            PlayerName = playerName;
            this.cpuMoveDecider = cpuMoveDecider;
            _repository = repository;
        }

        public void PlayRound()
        {
            Console.WriteLine("1. Rock\n2. Paper\n3. Scissors");
            string? playerChoice = null;
            int player = 0;
            while (playerChoice == null || playerChoice.Length <= 0)
            {
                Console.Write("What's your choice? ");
                playerChoice = Console.ReadLine();
                bool validchoice = int.TryParse(playerChoice, out player);
                if (!validchoice || (player > 3 && player < 0))
                {
                    Console.WriteLine("Invalid Choice, Try Again!");
                    Console.WriteLine();
                    playerChoice = null;
                    continue;
                }
            }

            Move pcChoice = cpuMoveDecider.DecideMove();
            Console.WriteLine();
            Move playerMove = (Move)(player - 1);
            Console.WriteLine($"You choose [{playerMove}]");
            Console.WriteLine($"PC gives you a [{pcChoice}]");
            AddRecord(pcChoice, playerMove);
        }

        /// <summary>
        /// Add a round record to this game's memory
        /// </summary>
        /// <param name="pc">The move played by the PC in that round</param>
        /// <param name="player">The move played by the player in that round</param>
        private void AddRecord(Move pc, Move player)
        {
            var record = new Round(DateTimeOffset.Now, player, pc);
            _repository.AddNewRound(PlayerName, null, record);
            Console.WriteLine($"You have a {record.Result}!");
        }

        public string Summary()
        {
            IEnumerable<Round> allRecords = _repository.GetAllRoundsOfPlayer(PlayerName);
            var summary = new StringBuilder();
            summary.AppendLine($"Date\t\t\tComputer\t{PlayerName}\t\tResult");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allRecords)
            {
                summary.AppendLine($"{record.Date}\t{record.Player1}\t\t{record.Player2}\t\t{record.Result}");
            }
            summary.AppendLine("---------------------------------------------------------------");

            return summary.ToString();
        }
    }
}
