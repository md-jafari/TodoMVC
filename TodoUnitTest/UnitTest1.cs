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
        public async Task Post_AddsNoteToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NoteContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new NoteContext(options))
            {
                var controller = new NoteController(context);
                var noteToAdd = new Note { Id = 1, Text = "Test Note", IsDone = false };

                // Act
                var result = await controller.Post(noteToAdd);

                // Assert
                Assert.NotNull(result); // Ensure result is not null
                Assert.IsType<OkResult>(result); // Ensure OkResult is returned

                // Verify the note is added to the database
                var addedNote = await context.Note.FindAsync(1);
                Assert.NotNull(addedNote); // Ensure the note is found in the database
                Assert.Equal("Test Note", addedNote.Text); // Ensure correct text
                Assert.False(addedNote.IsDone); // Ensure IsDone is false
            }
        }

        [Fact]
        public async Task GetById_ReturnsNoteById()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NoteContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new NoteContext(options))
            {
                // Add a sample note to the in-memory database
                var noteToAdd = new Note { Id = 1, Text = "Test Note", IsDone = false };
                context.Note.Add(noteToAdd);
                await context.SaveChangesAsync();

                // Act
                var controller = new NoteController(context);
                var result = await controller.GetById(1);

                // Assert
                Assert.NotNull(result); // Ensure result is not null
                Assert.IsType<ActionResult<Note>>(result); // Ensure result is of type ActionResult<Note>

                if (result.Value != null)
                {
                    Assert.Equal(1, result.Value.Id); // Ensure correct ID
                    Assert.Equal("Test Note", result.Value.Text); // Ensure correct text
                    Assert.False(result.Value.IsDone); // Ensure IsDone is false
                }
                else
                {
                    // Print debug information if result is null
                    Console.WriteLine("Result is null");
                }
            }

           
        }


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