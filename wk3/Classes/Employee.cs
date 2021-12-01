namespace SampleNamespace
{
    public class Employee : Person
    {
        public int HoursWorked;
        public double PayRate;

        //Constructor
        public Employee(int HoursWorked, double PayRate, string firstName, string lastName) : base()
        {
            this.HoursWorked = HoursWorked;
            this.PayRate = PayRate;
        }

        public void doWork()
        {
            Console.WriteLine($"I've worked {HoursWorked} hours.");
        }
    }
}