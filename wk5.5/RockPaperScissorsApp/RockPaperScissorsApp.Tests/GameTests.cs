using System;
using System.Collections.Generic;
using Moq;
using RockPaperScissorsApp.App;
using RockPaperScissorsApp.DataInfrastructure;
using RockPaperScissorsApp.Logic;
using Xunit;

namespace RockPaperScissorsApp.Tests
{
    public class GameTests
    {
        [Fact]
        public void Summary_ZeroRounds_CorrectFormat()
        {
            // arrange
            string playerName = "asdf";
            Mock<IMoveDecider> mockDecider = new();
            Mock<IRepository> mockRepo = new();

            // lambda expression syntax: like an anonymous classless method (this kind of value in C# is called a "delegate")
            // the Mock class sets up its inner object using these method calls (Setup, Returns) and some magic called "reflection"
            mockRepo.Setup(x => x.GetAllRoundsOfPlayer(playerName)).Returns(Array.Empty<Round>());

            var game = new Game(playerName, mockDecider.Object, mockRepo.Object);

            // act
            string result = game.Summary();

            // assert
            var expected = "Date\t\t\tComputer\tasdf\t\tResult\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected: expected, actual: result);
        }

        [Fact]
        public void Summary_TwoRounds_CorrectFormat()
        {
            // arrange
            string playerName = "asdf";
            Round round1 = new(DateTimeOffset.FromUnixTimeSeconds(0), Move.Paper, Move.Paper);
            Round round2 = new(DateTimeOffset.FromUnixTimeSeconds(0), Move.Paper, Move.Paper);
            Mock<IMoveDecider> mockDecider = new();
            Mock<IRepository> mockRepo = new();
            mockRepo.Setup(x => x.GetAllRoundsOfPlayer(playerName)).Returns(new[] { round1, round2 });
            var game = new Game(playerName, mockDecider.Object, mockRepo.Object);

            // act
            string result = game.Summary();

            // assert
            var expected = "Date\t\t\tComputer\tasdf\t\tResult\r\n---------------------------------------------------------------\r\n1/1/1970 12:00:00 AM +00:00\tPaper\t\tPaper\t\tTie\r\n1/1/1970 12:00:00 AM +00:00\tPaper\t\tPaper\t\tTie\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected: expected, actual: result);
        }
    }
}
