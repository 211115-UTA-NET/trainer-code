using System;
using System.Collections.Generic;
using RockPaperScissorsApp.App;
using RockPaperScissorsApp.DataInfrastructure;
using RockPaperScissorsApp.Logic;
using Xunit;

namespace RockPaperScissorsApp.Tests
{
    public class GameTestsWithoutMoq
    {
        // given that we're writing a unit test, my test of "Game" should not involve the "SqlRepository" class
        //    and should DEFINITELY not involve a whole database, and ABOVE ALL should not involve the
        //    actual real database the app uses to run normally
        [Fact]
        public void Summary_ZeroRounds_CorrectFormat()
        {
            // arrange
            string playerName = "asdf";
            FakeRepository fakeRepo = new();
            var game = new Game(playerName, new FakeDecider(), fakeRepo);

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
            FakeRepository fakeRepo = new() { Rounds = new[] { round1, round2 } };
            var game = new Game(playerName, new FakeDecider(), fakeRepo);

            // act
            string result = game.Summary();

            // assert
            var expected = "Date\t\t\tComputer\tasdf\t\tResult\r\n---------------------------------------------------------------\r\n1/1/1970 12:00:00 AM +00:00\tPaper\t\tPaper\t\tTie\r\n1/1/1970 12:00:00 AM +00:00\tPaper\t\tPaper\t\tTie\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected: expected, actual: result);
        }
    }

    // more or less synonyms: "test double", "fake", "stub", "mock"

    public class FakeRepository : IRepository
    {
        public IEnumerable<Round> Rounds { get; set; } = Array.Empty<Round>();

        public IEnumerable<Round> GetAllRoundsOfPlayer(string name) => Rounds;

        public void AddNewRound(string? player1, string? player2, Round round) => throw new NotImplementedException();
    }

    public class FakeDecider : IMoveDecider
    {
        public Move DecideMove() => throw new NotImplementedException();
    }
}
