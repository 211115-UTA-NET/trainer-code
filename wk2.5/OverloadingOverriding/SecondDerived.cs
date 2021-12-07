namespace OverrideOverload
{
    public class SecondDerived : Derived
    {
        public string secondDerivedString;

        public SecondDerived()
        {
            this.secondDerivedString = "Derived";
        }

        //AccessModifier Modifier ReturnType MethodName(Parameters)
        public override void newMethod() //"override" is REQUIRED to extend or modify the virtual implementation of an 
                                         // inherited method, property, indexer, or event.
        {
            Console.WriteLine("Running newMethod() from SecondDerived.");
        }
    }

}