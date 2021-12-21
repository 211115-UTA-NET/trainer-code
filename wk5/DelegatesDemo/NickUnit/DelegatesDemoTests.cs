namespace NickUnit
{
    public class DelegatesDemoTests
    {
        [UnitTest]
        public void CommaFormat_Formats_0()
        {
            // arrange
            // act
            string result = DelegatesDemo.Program.CommaFormat(0);

            // assert
            string correct = "0, ";
            if (result != correct)
            {
                throw new InvalidOperationException($"Test failure, {result} should be {correct}");
            }
        }

        [UnitTest]
        public void FailingTest()
        {
            throw new InvalidOperationException("Test failure");
        }

        [ParametrizedUnitTest]
        [InlineData(1)]
        [InlineData(-1)]
        public void CommaFormat_Formats_Numbers(int num)
        {
            // arrange
            // act
            string result = DelegatesDemo.Program.CommaFormat(0);

            // assert
            string correct = "0, ";
            if (result != correct)
            {
                throw new InvalidOperationException($"Test failure, {result} should be {correct}");
            }
        }

        public void NotAUnitTest()
        {
            Console.WriteLine("don't run this one");
        }
    }
}
