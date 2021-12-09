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
        public DateTime Date { get; }

        public string PC { get; }
        public string Player { get; }
        public string result { get; }
        // constructor
        public Record(DateTime date, string pc, string player, string result)
        {
            this.Date = date;
            this.PC = pc;
            this.Player = player;
            this.result = result;
        }

        public Record(Serialization.Record xmlRecord)
        {
            Date = xmlRecord.When;
            PC = xmlRecord.CPUMove ?? throw new ArgumentException("CPU move cannot be null", nameof(xmlRecord));
            Player = xmlRecord.PlayerMove ?? throw new ArgumentException("Player move cannot be null", nameof(xmlRecord));
            result = xmlRecord.Result ?? throw new ArgumentException("Result cannot be null", nameof(xmlRecord));
        }
    }
}
