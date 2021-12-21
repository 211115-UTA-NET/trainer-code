namespace DelegatesDemo
{
    // this defines a new delegate type - valid values
    // are any method that has one int parameter and returns string.
    public delegate string NumberFormatter(int num);

    // this defines a new class type- valid values are
    // any object produced by its (default) constructor
    public class Program
    {

        public static void Main()
        {
            // calling the method - wrong
            //NumberFormatter formatter = CommaFormat();
            // referencing the method - correct
            NumberFormatter formatter = CommaFormat;
            PrintNumbers(new List<int> { 1, 2, 3, 4 }, formatter);
        }

        // C# borrows a little from "functional programming" (as opposed to OOP)
        //    whereby functions/behavior are just another kind of data

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
