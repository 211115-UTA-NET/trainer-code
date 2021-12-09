using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xml = RockPaperScissorsApp.App.Serialization;

namespace RockPaperScissorsApp.App
{
    public class Game
    {
        private List<Round> allRecords = new List<Round>();
        public string PlayerName { get; }
        private string[] RPS = { "Rock", "Paper", "Scissor" };

        public XmlSerializer Serializer { get; } = new(typeof(List<Xml.Record>));

        // constructor
        public Game(string playerName, List<Round>? allRecords = null)
        {
            this.PlayerName = playerName;
            if (allRecords != null)
            {
                this.allRecords = allRecords;
            }
        }

        public void PlayRound()
        {
            Console.WriteLine("1. Rock\n2. Paper\n3. Scissor");
            string? playerChoice = null;
            int player=0;
            while (playerChoice == null || playerChoice.Length <= 0 )
            {
                Console.Write("What's your choice? ");
                playerChoice = Console.ReadLine();
                bool validchoice = int.TryParse(playerChoice, out player);
                if (!validchoice || (player > 3 && player < 0) )
                {
                    Console.WriteLine("Invalid Choice, Try Again!");
                    Console.WriteLine();
                    playerChoice=null;
                    continue;
                }
            }

            if (player < 4 && player > 0)
            {
                Random random = new Random();
                int PCchoice = random.Next(1, 4);
                Console.WriteLine();
                Console.WriteLine($"You choose [{RPS[player - 1]}]");
                Console.WriteLine($"PC gives you a [{RPS[PCchoice - 1]}]");
                // Rock !< Scissor
                if(PCchoice == 1 && player == 3)
                {
                    AddRecord(PCchoice, player, "Lose");
                }
                else if (PCchoice < player )
                {
                    AddRecord(PCchoice, player, "Win");
                }
                else if (PCchoice > player)
                {
                    AddRecord(PCchoice, player, "Lose");
                }
                else
                {
                    AddRecord(PCchoice, player, "A Tie");
                }
            }
        }
        private void AddRecord(int pc, int player, string result)
        {
            Move move1 = (Move)Enum.Parse(typeof(Move), RPS[pc - 1]);
            Move move2 = (Move)Enum.Parse(typeof(Move), RPS[player - 1]);

            var record = new Round(DateTime.Now, move1, move2);
            allRecords.Add(record);
            if (result == "A Tie")
            {
                Console.WriteLine($"You have {result}!");
            }
            else
            {
                Console.WriteLine($"You {result}!");
            }
        }

        public void Summary()
        {
            var summary = new StringBuilder();
            summary.AppendLine($"Date\t\t\tComputer\t{PlayerName}\t\tResult");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allRecords)
            {
                summary.AppendLine($"{record.Date}\t{record.Player1}\t\t{record.Player2}\t\t{record.Result}");
            }
            summary.AppendLine("---------------------------------------------------------------");
            
            Console.WriteLine(summary.ToString());
        }

        public string SerializeAsXml()
        {
            var xmlRecords = new List<Xml.Record>();

            foreach (Round record in allRecords)
            {
                //var xml = new Xml.Record();
                //xml.When = record.Date;
                //xml.PlayerMove = record.Player;
                //xml.CPUMove = record.PC;
                //xml.Result = record.result;
                // "property initializer" syntax - call a constructor & set properties in one go.
                xmlRecords.Add(new Xml.Record
                {
                    When = record.Date,
                    PlayerMove = record.Player1.ToString(),
                    CPUMove = record.Player2.ToString(),
                    Result = record.Result.ToString()
                });
            }

            var stringWriter = new StringWriter();
            Serializer.Serialize(stringWriter, xmlRecords);
            stringWriter.Close();
            return stringWriter.ToString();
        }
    }
}
