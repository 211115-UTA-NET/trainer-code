using DelegatesDemo.Extensions;

namespace DelegatesDemo
{
    // this defines a new delegate type - valid values
    // are any method that has one int parameter and returns string.
    public delegate string NumberFormatter(int num);
    // ^ equivalent to Func<int, string>

    public delegate T2 Mapper<T, T2>(T value);

    // these days in C#, we don't have to define new delegate types
    // unless we want to, to give them a special name.

    // there are builtin generic delegate types to cover all use cases
    // Func<T1, T2, T3, ...> (for all non-void return methods) T1, T2 are the parameter types, the last one is the return type
    // Action<T1, T2, T3, ...> (for all void return methods) T1, T2, T3 are the parameter types

    // this defines a new class type- valid values are
    // any object produced by its (default) constructor
    public class Program
    {
        public static void TryOutExtensionMethods()
        {

        }
        // these days in C#, instead of anonymous delegate like delegate (int num) { return $"{num} "; }),
        // we can use lambda expressions (much more succinct) like x => $"{x} ";
        // num => { return $"{num} "; }

        public static void Main()
        {
            Console.WriteLine(10.Square());
            Console.WriteLine(5.Multiply(2, 3)); // 30
            IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4 };
            numbers = numbers.Map(x => x.Square());
            // calling the method - wrong
            //NumberFormatter formatter = CommaFormat();
            // referencing the method - correct
            //NumberFormatter formatter = CommaFormat;
            //Func<int, string> formatter = delegate (int num) { return $"{num} "; };
            Func<int, string> formatter = CommaFormat;
            PrintNumbers(numbers, formatter);

            var list = new List<string> { "a", "b", "c" };
            list.Map(x =>
            {
                //if (x == 1) { }
                return x.ToUpper();
            });
        }

        // C# borrows a little from "functional programming" (as opposed to OOP)
        //    whereby functions/behavior are just another kind of data

        // if you want to declare something with delegate type, you need to define that specific delegate type
        //     ... OR use the builtin Func / Action
        public static void PrintNumbers(IEnumerable<int> numbers, Func<int, string> formatter)
        {
            Console.Write("[");
            numbers.RunForEach(delegate (int num) { Console.Write(formatter(num)); });
            Console.WriteLine("]");
        }

        public static string CommaFormat(int num)
        {
            return $"{num}, ";
        }

        public static void BadMethod(int num)
        {
        }

        public static void FuncActionLinqStuff()
        {

            // Func<T> & friends are generic delegate types.
            // Func<T> is for functions that take no parameters and return T.
            // Func<T> is for functions that take no parameters and return T.
            // Func<T1, T> is for functions that take one parameter of type T1 and return T.

            // Action is for functions that take no parameters and return void.
            // Action<T1> is for functions that take one parameter of type T1 and return void.

            Action<string> print = x => Console.WriteLine(x);

            Func<int, int, int> add = (x, y) => x + y;

            // Predicate<T> is basically the same as Func<T, bool>
            //Predicate<int>  

            var seven = add(3, 4);
            
            // LINQ methods never modify the original sequence/collection
            // LINQ will not actually run though the operations you specify (select, where, order by)
            // until you "materialize" a value, until you need a concrete value
            //    until then, the execution is "deferred"

            List<int> numbers = new List<int> { 1, 2, 3, 4, 6, 8, 4, 3 };
            IEnumerable<int> filtered = numbers.Where(x => x > 5); // LINQ Where - filtering a collection
            filtered = filtered.OrderByDescending(x => x); // re-order the sequence in descending order.

            List<int> filteredList = filtered.ToList(); // { 6, 8 }
            // only at that point just now, was the list filtered and sorted.

            // can do it all in one statement like this
            List<int> filteredList2 = numbers
                .Where(x => x > 5)
                .Select(x => x - 2)
                .OrderByDescending(x => x)
                .ToList();

            // LINQ has not just "method syntax" (what we've been using)
            // it also has "query syntax" for people that like to look at sql
            List<int> filteredList3 = (from x in numbers
                                       where x > 5
                                       orderby -x
                                       select x - 2).ToList();

            // LINQ doesn't just run in-memory like this on .NET objects... (aka LINQ To Objects)
            // there is also LINQ To SQL (where your lambda expressions are converted to SQL statements and sent to a server, used by Entity Framework)
            //  LINQ To XML where they parse through some XML file
            // crazy things like that using a special interface called IQueryable

            List<string> strings = new List<string> { "asd", "bcd", "tffq" };
            strings.OrderBy(x => x.Length); // length order
            strings.OrderBy(x => x); // lexicographic order / dictionary order

            strings.Min(); // 'minimum in dictionary order of the strings
            strings.Min(x => x); // 'minimum in dictionary order of the strings
            strings.Min(x => x.Length); //  minimum length of a string
            strings.MinBy(x => x.Length); //  shortest string

            // First is like Where, but returns only the first matching value

            // LINQ:
            //    three kinds of functions in LINQ.
            //  1. the ones that return one distinct value. these functions process the sequence immediately.
            //      ex: Count, Min, Max, First
            //  2. the ones that return IEnumerable (a sequence). these functions use "deferred execution".
            //     ex: OrderBy, Where, Select, Skip, Take
            //  3. thes ones that return some concrete collection. these also process immediately.
            //     ex: ToList

            // it's usual in functional programming to, instead of MODIFYING data, return NEW data
            // so LINQ methods of the 2nd type never modify the original sequence. they always return a new one

            strings.OrderBy(x => x.Length) // deferred execution
                .Select(x => x[0]) // deferred execution
                .First();         // sort based on length, only needs to index into one of the strings, not all.
        }
    }
}
