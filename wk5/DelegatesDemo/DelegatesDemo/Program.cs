using DelegatesDemo.Extensions;

namespace DelegatesDemo
{
    // this defines a new delegate type - valid values
    // are any method that has one int parameter and returns string.
    public delegate string NumberFormatter(int num);

    public delegate T Mapper<T>(T value);

    // this defines a new class type- valid values are
    // any object produced by its (default) constructor
    public class Program
    {
        public static void TryOutExtensionMethods()
        {

        }

        public static void Main()
        {
            Console.WriteLine(10.Square());
            Console.WriteLine(5.Multiply(2, 3)); // 30
            IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4 };
            numbers = numbers.Map(delegate(int x) { return x.Square(); });
            // calling the method - wrong
            //NumberFormatter formatter = CommaFormat();
            // referencing the method - correct
            //NumberFormatter formatter = CommaFormat;
            NumberFormatter formatter = delegate (int num) { return $"{num} "; };
            PrintNumbers(numbers, formatter);

            var list = new List<string> { "a", "b", "c" };
            list.Map(delegate (string x) { return x.ToUpper(); });
        }

        // C# borrows a little from "functional programming" (as opposed to OOP)
        //    whereby functions/behavior are just another kind of data

        // if you want to declare something with delegate type, you need to define that specific delegate type
        public static void PrintNumbers(IEnumerable<int> numbers, NumberFormatter formatter)
        {
            Console.Write("[");
            foreach (int num in numbers)
            {
                string formatted = formatter(num);
                Console.Write(formatted);
            }
            Console.WriteLine("]");
        }

        public static string CommaFormat(int num)
        {
            return $"{num}, ";
        }

        public static void BadMethod(int num)
        {
        }
    }
}
