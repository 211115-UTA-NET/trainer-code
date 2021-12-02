namespace MyNamespace
{
    class DerivedClass : BaseClass
    {
        public override void AbstractMethod()
        {
            _baseString = "Base+Derived";
        }

        // public override string BaseString
        // {
        //     get
        //     {
        //         return "This was from the derived getter";
        //     }
        // }


    }
}