using System;
using RockPaperScissorsApp.Logic;
using Xunit;
using Xunit.Sdk;

namespace RockPaperScissorsApp.Tests
{
    // common to have one test class for each actual class
    public class RoundTests
    {
        // https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
        // typical naming convention for test methods:
        // UnitOfTest_TestCondition_CorrectBehavior

        [Fact]
        public void EvaluateResult_RockScissors_Win()
        {
            // unit tests are supposed to be laser focused on one particular slice of behavior of one small unit of code.
            // to keep us focused, we divide the test logically in three steps: "arrange, act, assert"

            // ARRANGE (any set up to get ready for that one slice of behavior)
            //   (none for this method)

            // ACT (the behavior under test)
            var result = Round.EvaluateResult(Move.Rock, Move.Scissors);
            // ^ errors were here because... (1) namespaces aren't connected (2) projects aren't connected
            // fix #2 with a project reference... if A has a reference to B, A can use B's classes (but NOT vice versa)
            // in the case of testing, it's easy... the tests always reference the main code, never the reverse

            // ASSERT (checking that the behavior was as expected)
            Assert.Equal(expected: RoundResult.Win, actual: result);
        }

        [Fact]
        public void Result_RockScissors_Win()
        {
            // arrange
            var round = new Round(DateTime.Now, Move.Rock, Move.Scissors);

            // act
            var result = round.Result;

            // ASSERT (checking that the behavior was as expected)
            Assert.Equal(expected: RoundResult.Win, actual: result);
        }
    }
}
