namespace DelegatesDemo
{
    // this defines a new delegate type - valid values
    // are any method that has one int parameter and returns string.
    public delegate string NumberFormatter(int num);

    public delegate int NumberMapper(int num);

    // this defines a new class type- valid values are
    // any object produced by its (default) constructor
    public class Program
    {

        public static void Main()
        {
            var numbers = new List<int> { 1, 2, 3, 4 };
            numbers = Map(numbers, delegate(int x) { return x * x; });
            // calling the method - wrong
            //NumberFormatter formatter = CommaFormat();
            // referencing the method - correct
            //NumberFormatter formatter = CommaFormat;
            NumberFormatter formatter = delegate (int num) { return $"{num} "; };
            PrintNumbers(numbers, formatter);
        }

        // C# borrows a little from "functional programming" (as opposed to OOP)
        //    whereby functions/behavior are just another kind of data

        // let's define something actually useful in many situations - 
        // a method the help us transform or map a list to a new value for each element of that list
        public static List<int> Map(List<int> list, NumberMapper mapper)
        {
            List<int> result = new();
            foreach (int item in list)
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
