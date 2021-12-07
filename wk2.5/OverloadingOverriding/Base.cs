namespace OverrideOverload
{
    public class Base
    {
        public string baseString;


        // Constructor
        public Base()
        {
            this.baseString = "Base";
        }
        public Base(string baseString)
        {
            this.baseString = baseString;
        }
            // Overloading - creating multipleversions of a method or constructor, that accept different
            // parameters (number of parameters, types, etc) and accomplish the same task.

        public void speak()
        {
            Console.WriteLine("Hello, I am a Base class object.");
        }
        public string speak(string type)
        {
            Console.WriteLine($"Hello, I am a {type} object.");
            return "done";
        }
        // public void speak(string Type, int number)
        // {
        //     Console.WriteLine($"Hello, I am a {Type} object.{number}");
        // }
        // public void speak(string Type, int number, int otherNumber)
        // {
        //     Console.WriteLine($"Hello, I am a {Type} object. {number} {otherNumber}");
        // }



        // Use descriptive names for the methods, but the methods to be overloaded MUST have the same name.
        // Use descriptive names for the parameters. Use the same name for the same parameter for each instance
        //      of an overloaded method.
        // Send parameters in a consistent order.
        // Do NOT have overloads with parameters at the same position and similar types, yet with different semantics.



        //AccessModifier Modifier ReturnType MethodName(Parameters)
        public virtual void newMethod()     //the "virtual" keyword is used to identify a method to allow it to be overridden.
        {
            Console.WriteLine("Running newMethod() from Base.");
        }
    }
}