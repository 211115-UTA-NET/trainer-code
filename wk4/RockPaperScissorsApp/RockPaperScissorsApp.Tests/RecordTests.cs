using System;
using Xunit;
using Xunit.Sdk;

namespace RockPaperScissorsApp.Tests
{
    public class RecordTests
    {
        // it's just as important to test that "invalid" scenarios are handled correctly
        // as it is to test the more obvious "valid" scenarios
        [Fact]
        public void CreateRound_InvalidPlayerMove_ThrowsError()
        {
            // arrange
            var xml = new App.Serialization.Record
            {
                PlayerMove = "asdfasdf",
                CPUMove = "Rock",
                Result = "Win"
            };

            // act
            try
            {
                var round = xml.CreateRound();
            }
            catch (ArgumentException)
            {
                return;
            }
            throw new XunitException("expected an ArgumentException");

            // the cool way to write that ^
            //Assert.ThrowsAny<ArgumentException>(() => new Round(xml));

            // assert
            // (here, the correct behavior is throwing an exception.
            // if xunit catches an unhandled exception, it is treated as a failure.
        }
    }
}
