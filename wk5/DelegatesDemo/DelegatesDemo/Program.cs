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
            var numbers = new List<int> { 1, 2, 3, 4 };
            numbers = Map(numbers, delegate(int x) { return x.Square(); });
            // calling the method - wrong
            //NumberFormatter formatter = CommaFormat();
            // referencing the method - correct
            //NumberFormatter formatter = CommaFormat;
            NumberFormatter formatter = delegate (int num) { return $"{num} "; };
            PrintNumbers(numbers, formatter);

            Map(new List<string> { "a", "b", "c" }, delegate (string x) { return x.ToUpper(); });
        }

        // C# borrows a little from "functional programming" (as opposed to OOP)
        //    whereby functions/behavior are just another kind of data

        // extension methods are a C# syntax that let's you take static methods
        // and seemingly attach them to already-defined data types, even builtin ones or sealed ones
        // they're not REALLY attached to those types, it's only a matter of nice-looking syntax.
        //   (you can only use an extension method when the static class that defines it is
        //    in the same namespace or referenced with a using directive).
        // extension methods are just a nice way to define helper methods
        //    you can't do inheritance with them

        // let's define something actually useful in many situations - 
        // a method the help us transform or map a list to a new value for each element of that list
        public static List<T> Map<T>(List<T> list, Mapper<T> mapper)
        {
            List<T> result = new();
            foreach (T item in list)
            {
                result.Add(mapper(item));
            }
            return result;
        }

        // if you want to declare something with delegate type, you need to define that specific delegate type
        public static void PrintNumbers(List<int> numbers, NumberFormatter formatter)
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
