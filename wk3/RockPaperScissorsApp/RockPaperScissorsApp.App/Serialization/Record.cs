using System.Xml.Serialization;

namespace RockPaperScissorsApp.App.Serialization
{
    public class Record
    {
        [XmlAttribute]
        public DateTime When { get; set; }
        public string? CPUMove { get; set; }
        public string? PlayerMove { get; set; }
        public string? Result { get; set; }
    }
}
