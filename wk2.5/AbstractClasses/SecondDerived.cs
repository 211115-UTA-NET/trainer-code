namespace MyNamespace
{
    class SecondDerived : DerivedClass
    {
        public override void AbstractMethod()
        {
            Console.WriteLine("Now for something completely different!");
        }

        // public override string BaseString
        // {
        //     get
        //     {
        //         return _baseString;
        //     }
        // }
    }
}