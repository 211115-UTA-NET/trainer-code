namespace SampleNamespace
{
    public class Trainee : Employee
    {
        bool inTraining = true;

        public Trainee()
        {
            this.inTraining = true;
        }

        public bool getInTraining()
        {
            return this.inTraining;
        }

    }
}