namespace SampleNamespace
{
    public class Customer : Person
    {
        public double CashOnHand {get; set;} = 0;


        public Customer( double CashOnHand, string firstName, string lastName) : base(firstName, lastName)
        {
            this.CashOnHand = CashOnHand;
        }


    }

}