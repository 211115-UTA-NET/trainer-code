namespace DelegatesDemo.Extensions
{
    public static class ExtensionMethods
    {
        // not an extension method
        // called like 'ExtensionMethods.Square(x)'
        //public static int Square(int num)
        //{
        //    return num * num;
        //}

        // an extension method
        // called like 'x.Square()'
        public static int Square(this int num)
        {
            return num.Multiply(num);
        }

        public static int Multiply(this int num, int other, int third = 1)
        {
            return num * other * third;
        }

        // extension methods are a C# syntax that let's you take static methods
        // and seemingly attach them to already-defined data types, even builtin ones or sealed ones
        // they're not REALLY attached to those types, it's only a matter of nice-looking syntax.
        //   (you can only use an extension method when the static class that defines it is
        //    in the same namespace or referenced with a using directive).
        // extension methods are just a nice way to define helper methods
        //    you can't do inheritance with them

        // let's define something actually useful in many situations - 
        // a method the help us transform or map a list to a new value for each element of that list
        //public static IEnumerable<T2> Map<T1, T2>(this IEnumerable<T1> list, Mapper<T1, T2> mapper)
        public static IEnumerable<TOutput> Map<TInput, TOutput>(this IEnumerable<TInput> items, Func<TInput, TOutput> mapper)
        {
            List<TOutput> result = new();
            //items.RunForEach(val => result.Add(mapper(val)));
            foreach (TInput item in items)
            {
                result.Add(mapper(item));
            }
            return result;
            // equiv with LINQ:
            //return items.Select(mapper);
        }

        // C# added a nice thing called LINQ, a BUNCH of general purpose extension methods on IEnumerable like that ^
        // in LINQ, my Map function is just called Select.
        // the LINQ namespace is System.Linq

        // not really needed if it's a List, because List literally already has this
        // method (called ForEach)
        // not very useful with the old anonymous delegate syntax because it's no less verbose
        public static void RunForEach<T>(this IEnumerable<T> list, Action<T> code)
        {
            foreach (T item in list)
            {
                code(item);
            }
        }
    }
}
