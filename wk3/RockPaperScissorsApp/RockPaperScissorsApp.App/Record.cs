using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsApp.App
{
    public class Record
    {
        // XmlSerializer (and often other serializers)
        // expect classes that have a zero-argument constructor
        //    and they can fill in public properties via their setters.
        //  a class that ONLY has a zero-arg constructor and public get set properties is sometimes
        //    called a POCO (plain old CLR object)

        // ex: [ 10/7 Computer: Rock VS You: Paper => You Win! ]
        public DateTime Date { get; set; }
        public string PC { get; set; }
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
