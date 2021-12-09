using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsApp.App
{
    public interface MyInterface
    {
        public int Data { get; set; }
    }

    public class BaseClass : MyInterface
    {
        public virtual int Data { get; set; }

        public static implicit operator int(BaseClass b)
        {
            return new Random().Next();
        }
    }
    public class DerivedClass : BaseClass
    {
        public override int Data {
            get => 3;
            set { throw new InvalidOperationException(); }
        }
        public int MoreData { get; set; }
    }

    public class CastingDemo
    {
        // making use of polymorphism
        private static void PrintData(BaseClass a)
        {
            Console.WriteLine(a.Data);

            Console.WriteLine(((DerivedClass)a).MoreData); // explicit cast (downcasting)

            if (a is DerivedClass d) // safe way to do that ^
            {
                Console.WriteLine(d.MoreData);
            }
            // there's also an "as" operator, bit similar
        }

        public static void NonNumericCasting()
        {
            BaseClass based = new BaseClass();
            DerivedClass derived = new DerivedClass();

            // taking a more-specific-type object and putting it in a more general-type variable,
            // when accessing that object THROUGH that variable, you can only see the members
            //  known to the more general type. it's as if the variable is a mask the object is wearing
            // hiding some of its members from view
            BaseClass x = derived; // upcasting (implicit)
            
            //PrintData(based);
            PrintData(derived); // implicit cast (upcasting - from derived class "lower down" to base class "higher up" on the )
            PrintData(x);

            AddInts(based, based);
        }

        public static void NumericCasting()
        {
            double d = 2.5;
            int i = 3;

            // explicit casting (numeric), because it can lose data
            Console.WriteLine(AddInts((int)d, (int)d));
            // implicit casting (numeric), because it won't lose any data
            Console.WriteLine(AddDoubles(i, i));

            short x1 = 3;
            short x2 = 3;
            AddInts((int)x1, (int)x2);
        }

        public static int AddInts(short a, short b)
        {
            return a + b;
        }

        public static int AddInts(int a, int b)
        {
            return a + b;
        }
        public static double AddDoubles(double a, double b)
        {
            return a + b;
        }
    }
}
