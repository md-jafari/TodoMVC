# Todo MVC Web API Application

This repository contains a simple Todo Web API application built using ASP.NET Core. The application provides endpoints to manage notes/tasks in a todo list.

## Controller

The `NoteController` handles various HTTP requests related to managing notes/tasks.

### Endpoints

- **GET /notes/{id}**: Retrieves a note by its ID.
- **GET /notes**: Retrieves all notes or notes based on completion status (completed or not completed).
- **GET /remaining**: Retrieves the count of remaining tasks.
- **POST /notes**: Creates a new note.
- **POST /toggle-all**: Toggles the completion status of all notes.
- **POST /clear-completed**: Clears completed notes.
- **PUT /notes/{id}**: Updates the completion status of a note.
- **DELETE /notes/{id}**: Deletes a note by its ID.

  
## Dependencies

- EntityFramework
- linq2db
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Swashbuckle.AspNetCore



# Todo MVC Unit Test 

This project contains unit tests for the `NoteController` class in the Todo Web API application. The tests are written using xUnit and cover various scenarios to ensure the proper functioning of the controller methods.

## Controller Tests

### `DeleteNote_RemovesNoteFromDatabase`
- Verifies that the `DeleteNote` method removes a note from the database.
- Adds a sample note to an in-memory database, deletes the note using the controller method, and verifies its removal from the database.

### `Post_AddsNoteToDatabase`
- Tests the `Post` method to ensure that it adds a new note to the database.
- Adds a note using the controller method, verifies its addition to the database, and checks its properties.

### `GetById_ReturnsNoteById`
- Tests the `GetById` method to ensure it returns the correct note by ID.
- Adds a sample note to the in-memory database, retrieves the note using the controller method, and verifies its properties.

### `GetById_ReturnsNotFoundForInvalidId`
- Ensures that the `GetById` method returns a "Not Found" result for an invalid note ID.
- Attempts to retrieve a note with an invalid ID and asserts that the result is a "Not Found" response.


## Dependencies

- Microsoft.EntityFrameworkCore.InMemory
- Microsoft.NET.Test.Sdk
- xunit
- xunit.runner.visualstudio
