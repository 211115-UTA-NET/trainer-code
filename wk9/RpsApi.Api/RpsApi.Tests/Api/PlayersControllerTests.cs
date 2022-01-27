using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using RpsApi.Api.Controllers;
using RpsApi.DataStorage;
using Xunit;

namespace RpsApi.Tests.Api
{
    public class PlayersControllerTests
    {
        [Fact]
        public async void GetAllWithEmptyPlayersReturnsEmpty()
        {
            // arrange
            Mock<IRepository> mockRepo = new();
            mockRepo.Setup(r => r.GetAllPlayersAsync()).ReturnsAsync(Array.Empty<string>());
            var controller = new PlayersController(mockRepo.Object);

            // act
            IActionResult result = await controller.GetAll();

            // assert
            OkObjectResult objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            IEnumerable<string> body = Assert.IsAssignableFrom<IEnumerable<string>>(objectResult.Value);
            Assert.Empty(body);
        }

        [Fact]
        public async void GetAllWithSomePlayersReturnsPlayersInSameOrder()
        {
            // arrange
            List<string> players = new() { "qpy", "abc", "123" };
            Mock<IRepository> mockRepo = new();
            mockRepo.Setup(r => r.GetAllPlayersAsync()).ReturnsAsync(players);
            var controller = new PlayersController(mockRepo.Object);

            // act
            IActionResult result = await controller.GetAll();

            // assert
            OkObjectResult objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            IEnumerable<string> body = Assert.IsAssignableFrom<IEnumerable<string>>(objectResult.Value);
            List<string> list = body.ToList();
            Assert.Equal(expected: players, actual: list); // element-wise comparison
        }

        [Fact]
        public async void CreatePlayerWithDbUpdateExceptionReturn500()
        {
            // arrange
            string playerName = "asdf";
            Mock<IRepository> mockRepo = new();
            // would happen in case of duplicate name
            mockRepo.Setup(r => r.AddPlayerAsync(playerName)).ThrowsAsync(new DbUpdateException());
            var controller = new PlayersController(mockRepo.Object);

            // act
            IActionResult result = await controller.Add(playerName);

            // assert
            ObjectResult objectResult = Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(expected: 409, actual: objectResult.StatusCode);
        }
    }
}
