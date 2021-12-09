using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RockPaperScissorsApp.Logic;
using Xml = RockPaperScissorsApp.App.Serialization;

namespace RockPaperScissorsApp.App
{
    public class Game
    {
        private List<Round> allRecords = new List<Round>();
        public string PlayerName { get; }
        private readonly IMoveDecider cpuMoveDecider;

        public XmlSerializer Serializer { get; } = new(typeof(List<Xml.Record>));

        // constructor
        public Game(string playerName, IMoveDecider cpuMoveDecider, List<Round>? allRecords = null)
        {
            PlayerName = playerName;
            this.cpuMoveDecider = cpuMoveDecider;
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

            //Random random = new Random();
            //Move PCchoice = (Move)random.Next(3); // 0, 1 or 2
            Move pcChoice = cpuMoveDecider.DecideMove();
            Console.WriteLine();
            Move playerMove = (Move)(player - 1);
            Console.WriteLine($"You choose [{playerMove}]");
            Console.WriteLine($"PC gives you a [{pcChoice}]");
            AddRecord(pcChoice, playerMove);
        }

        private void AddRecord(Move pc, Move player)
        {
            var record = new Round(DateTime.Now, player, pc);
            allRecords.Add(record);
            Console.WriteLine($"You have a {record.Result}!");
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
