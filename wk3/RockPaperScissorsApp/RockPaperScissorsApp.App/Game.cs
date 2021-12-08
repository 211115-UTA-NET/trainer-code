using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RockPaperScissorsApp.App
{
    public class Game
    {
        private List<Record> allRecords = new List<Record>();
        public string PlayerName { get; }
        private string[] RPS = { "Rock", "Paper", "Scissor" };

        public XmlSerializer Serializer { get; } = new(typeof(List<Record>));

        // constructor
        public Game(string playerName, List<Record>? allRecords = null)
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
            var record = new Record(DateTime.Now, RPS[pc - 1], RPS[player - 1], result);
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
            var summary = new System.Text.StringBuilder();
            summary.AppendLine($"Date\t\t\tComputer\t{PlayerName}\t\tResult");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allRecords)
            {
                summary.AppendLine($"{record.Date}\t{record.PC}\t\t{record.Player}\t\t{record.result}");
            }
            summary.AppendLine("---------------------------------------------------------------");
            
            Console.WriteLine(summary.ToString());
        }

        public string SerializeAsXml()
        {
            var stringWriter = new StringWriter();
            Serializer.Serialize(stringWriter, allRecords);
            stringWriter.Close();
            return stringWriter.ToString();
        }
    }
}
