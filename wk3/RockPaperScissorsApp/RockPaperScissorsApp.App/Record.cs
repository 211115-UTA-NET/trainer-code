using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RockPaperScissorsApp.App
{
    public class Record
    {
        // XmlSerializer (and often other serializers)
        // expect classes that have a zero-argument constructor
        //    and they can fill in public properties via their setters.
        //  a class that ONLY has a zero-arg constructor and public get set properties is sometimes
        //    called a POCO (plain old CLR object)


        // C# has "attributes" - they don't do anything by themselves,
        // they're there for some other code to notice them and make some change to their own behavior

        // ex: [ 10/7 Computer: Rock VS You: Paper => You Win! ]
        [XmlAttribute(AttributeName = "When")]
        public DateTime Date { get; set; }

        [XmlElement(ElementName = "CPUMove")]
        public string PC { get; set; }
        [XmlElement(ElementName = "PlayerMove")]
        public string Player { get; set; }
        public string result { get; set; }
        // constructor
        public Record(DateTime date, string pc, string player, string result)
        {
            this.Date = date;
            this.PC = pc;
            this.Player = player;
            this.result = result;
        }

        public Record()
        {
        }
    }
}
