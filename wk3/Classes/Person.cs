//Namespace
namespace SampleNamespace
{
    //Another Class!
    // AccessModifier class ClassName
    public class Person
    {
        //Fields
        private string firstName;
        private string lastName;

        //Constructor(s)
        public Person()
        {
            firstName = "John";
            lastName = "Doe";
        }

        public Person( string first, string last )
        {
            firstName = first;
            lastName = last;
        }

        //Methods
        public void setFirstName(string name)
        {
            firstName = name;
        }

        public string getFirstName()
        {
            return firstName;
        }
 
        //accessModifier returnType methodName(Parameters)
        public void Introduce()
        {
            Console.WriteLine($"Hello, my name is {firstName} {lastName}");
        }
    }
}