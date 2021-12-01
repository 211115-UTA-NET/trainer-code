//Using statements 

using System;
using System.IO;
using System.Collections.Generic;

//Namespace
namespace SampleNamespace
{
//Class declaration
    class Program
    {
    //function declaration
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Person newGuy = new Person();
            // newGuy.Introduce();

            // Person someOtherPerson = new Person("Tommy", "Nguyen");
            // someOtherPerson.Introduce();

            // Console.WriteLine(newGuy.getFirstName());
            // newGuy.setFirstName("Stefan");
            // Console.WriteLine(newGuy.getFirstName());

            Employee Kyler = new Employee(40, 18.50, "Kyler", "Dennis");
            Kyler.doWork();
            Kyler.Introduce();
        }
    }
}