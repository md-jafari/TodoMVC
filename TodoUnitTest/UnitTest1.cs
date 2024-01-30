using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoMVC.Controllers;
using TodoMVC.Data;
using TodoMVC.Models;

namespace TodoUnitTest
{
    public class NoteControllerTests
    {
        [Fact]
        public async Task GetById_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NoteContext>()
                .UseInMemoryDatabase(databaseName: "xtestdb")
                .Options;

            using (var context = new NoteContext(options))
            {
                var controller = new NoteController(context);

                // Act
                var result = await controller.GetById(999);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }

        // Add more tests for other methods as needed
    }
}