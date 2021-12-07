namespace OverrideOverload
{
    public class Derived : Base
    {
        public string derivedString;

        public Derived()
        {
            this.derivedString = "Derived";
        }


        // Method overriding is a technique that allows the invoking of functions from another class (base)
        // in the derived class. Creating a method in the derived class with the same name as the method in the base class.



        //AccessModifier Modifier ReturnType MethodName(Parameters)
        public override void newMethod() //"override" is REQUIRED to extend or modify the virtual implementation of an 
                                         // inherited method, property, indexer, or event.
        {
            Console.WriteLine("Running newMethod() from Derived.");
        }


        // All override members...
        // - provide a new impleentation of an inherited method.
        // - must have the same signature as the inherited method.
        // - both methods must be virtual, abstract, or override.
        // - must NOT use the static or virtual modifiers to override a method.
    }
}