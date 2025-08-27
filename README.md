# Church Management System

A Domain-Driven Design (DDD) application for managing church members and activities.

## Architecture

This project follows Domain-Driven Design (DDD) principles with a clean architecture pattern, organized into the following layers:

### Domain Layer (`ChurchManagement.Domain`)
- **Entities**: Core business objects (e.g., Member)
- **Value Objects**: Immutable objects that represent concepts (e.g., Email)
- **Repositories**: Interfaces for data access contracts
- **Services**: Domain service interfaces
- **Events**: Domain events for business logic
- **Common**: Base classes and shared domain logic

### Application Layer (`ChurchManagement.Application`)
- **Commands**: Command objects for write operations
- **Queries**: Query objects for read operations
- **DTOs**: Data Transfer Objects for API contracts
- **Interfaces**: Application service contracts
- **Services**: Application services that orchestrate domain objects
- **Handlers**: Command and query handlers

### Infrastructure Layer (`ChurchManagement.Infrastructure`)
- **Repositories**: Concrete implementations of repository interfaces
- **Services**: External service implementations (email, events, etc.)
- **Data**: Data access configurations and contexts
- **Configuration**: Dependency injection setup

### Web Layer (`ChurchManagement.Web`)
- **Controllers**: API controllers
- **Program.cs**: Application startup and configuration

## Getting Started

### Prerequisites
- .NET 8.0 SDK

### Running the Application

1. Clone the repository
2. Navigate to the root directory
3. Build the solution:
   ```bash
   dotnet build
   ```

4. Run the Web API:
   ```bash
   cd src/ChurchManagement.Web
   dotnet run
   ```

5. Open your browser to `http://localhost:5070/swagger` to view the API documentation

### Project Structure

```
ChurchManagement/
├── src/
│   ├── ChurchManagement.Domain/
│   │   ├── Common/
│   │   │   ├── BaseEntity.cs
│   │   │   └── ValueObject.cs
│   │   ├── Entities/
│   │   │   └── Member.cs
│   │   ├── ValueObjects/
│   │   │   └── Email.cs
│   │   ├── Repositories/
│   │   │   └── IMemberRepository.cs
│   │   └── Services/
│   │       └── IDomainServices.cs
│   ├── ChurchManagement.Application/
│   │   ├── Commands/
│   │   │   └── MemberCommands.cs
│   │   ├── Queries/
│   │   │   └── MemberQueries.cs
│   │   ├── DTOs/
│   │   │   └── MemberDtos.cs
│   │   ├── Interfaces/
│   │   │   └── IMemberService.cs
│   │   └── Services/
│   │       └── MemberService.cs
│   ├── ChurchManagement.Infrastructure/
│   │   ├── Configuration/
│   │   │   └── DependencyInjection.cs
│   │   ├── Repositories/
│   │   │   └── InMemoryMemberRepository.cs
│   │   └── Services/
│   │       └── ExternalServices.cs
│   └── ChurchManagement.Web/
│       ├── Controllers/
│       │   └── MembersController.cs
│       └── Program.cs
└── tests/ (placeholder for future tests)
```

## API Endpoints

The application provides the following REST API endpoints for member management:

- `GET /api/members` - Get all members
- `GET /api/members/active` - Get active members only
- `GET /api/members/{id}` - Get member by ID
- `GET /api/members/by-email/{email}` - Get member by email
- `POST /api/members` - Create a new member
- `PUT /api/members/{id}` - Update member information
- `PATCH /api/members/{id}/activate` - Activate a member
- `PATCH /api/members/{id}/deactivate` - Deactivate a member
- `DELETE /api/members/{id}` - Delete a member
- `GET /api/members/count` - Get total member count

## Features

### Current Features
- Member management (CRUD operations)
- Member activation/deactivation
- Email validation with value objects
- In-memory data storage
- RESTful API with Swagger documentation
- Dependency injection setup
- Domain-driven design architecture

### Future Enhancements
- Database integration (Entity Framework Core)
- Authentication and authorization
- Event handling and messaging
- Additional domain entities (Events, Donations, etc.)
- Comprehensive unit and integration tests
- Logging and monitoring

## Development Notes

- The application currently uses in-memory storage for simplicity
- All business logic is encapsulated within the Domain layer
- The Infrastructure layer is designed to be easily replaced (e.g., switching from in-memory to database storage)
- The Web layer is kept thin and focused on HTTP concerns
- Proper separation of concerns is maintained throughout all layers
