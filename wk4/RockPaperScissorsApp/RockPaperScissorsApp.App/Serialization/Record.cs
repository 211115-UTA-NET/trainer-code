using System.Xml.Serialization;
using RockPaperScissorsApp.Logic;

namespace RockPaperScissorsApp.App.Serialization
{
    public class Record
    {
        [XmlAttribute]
        public DateTime When { get; set; }
        public string? CPUMove { get; set; }
        public string? PlayerMove { get; set; }
        public string? Result { get; set; }

        public Round CreateRound()
        {
            var player1 = (Move)Enum.Parse(typeof(Move), CPUMove ?? throw new InvalidOperationException("CPU move cannot be null"));
            var player2 = (Move)Enum.Parse(typeof(Move), PlayerMove ?? throw new InvalidOperationException("Player move cannot be null"));
            return new Round(date: When, player1: player1, player2: player2);
        }
    }
}
