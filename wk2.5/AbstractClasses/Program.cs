using System;

namespace MyNamespace
{
    class Program
    {
        public static void Main()
        {
            // DerivedClass obj;
            // obj = new DerivedClass();

            // Console.WriteLine(obj.BaseString);
            // obj.AbstractMethod();
            // Console.WriteLine(obj.BaseString);

            SecondDerived anotherObject;
            anotherObject = new SecondDerived();
            
            Console.WriteLine(anotherObject.BaseString);
            anotherObject.AbstractMethod();

            // DerivedClass YetAnother;
            // YetAnother = new DerivedClass();
            // Console.WriteLine(YetAnother.BaseString);




        }
    }
}