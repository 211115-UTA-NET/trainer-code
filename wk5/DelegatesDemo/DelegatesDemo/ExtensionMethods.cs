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
        public static IEnumerable<T> Map<T>(this IEnumerable<T> list, Mapper<T> mapper)
        {
            List<T> result = new();
            foreach (T item in list)
            {
                result.Add(mapper(item));
            }
            return result;
        }
    }
}
