# Project Guidelines

This repository uses the following conventions and requirements:

- **Architecture and Design**
  - Follow Domain-Driven Design (DDD) for structuring modules and boundaries.
  - Apply SOLID principles in all code.
  - Favor explicit interfaces and loose coupling.

- **Testing**
  - Every change must include accompanying unit tests.
  - Run `dotnet test` before committing; all tests must pass.
  - Maintain at least 85% code coverage for the affected projects. Use a coverage tool such as [coverlet](https://github.com/coverlet-coverage/coverlet) and fail the build if coverage drops below 85%.

- **Database and Data Access**
  - Use [Dapper](https://github.com/DapperLib/Dapper) for all SQL/database access.
  - All queries must be parameterized.

- **Frontend**
  - Use Razor Pages for UI.
  - Use Bootstrap for styling.

- **Security and Quality**
  - Do not commit secrets or credentials; use configuration or environment variables.
  - Validate and sanitize all user inputs.
  - Prefer asynchronous APIs and dispose resources correctly.

- **Other Guidelines**
  - Keep commit messages descriptive.
  - Run any additional linters or formatters (`dotnet format`, etc.) as needed to keep a consistent code style.

