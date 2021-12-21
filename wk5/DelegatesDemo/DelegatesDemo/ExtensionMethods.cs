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
    }
}
