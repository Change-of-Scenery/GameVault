# GitHub Copilot Instructions for GameVault

## Project Overview
GameVault is a .NET 10 Blazor Web Application for managing a collection of video games. The project includes both a Blazor frontend and Web API endpoints.

## Technology Stack
- **Framework**: .NET 10
- **Project Type**: Blazor Server (Interactive Server Components)
- **C# Version**: 14.0
- **API Documentation**: Scalar with OpenAPI

## Project Structure and Best Practices

### Directory Organization
```
GameVault/
├── Components/          # Blazor components
├── Controllers/         # Web API controllers
├── Models/             # Data models and DTOs
├── Services/           # Service interfaces and implementations
├── Program.cs          # Application entry point
└── GameVault.csproj    # Project file
```

### Coding Standards

#### File Organization
- **One class per file**: Each model, controller, or component should be in its own file
- **Namespace alignment**: Namespaces should match the folder structure (e.g., `GameVault.Models`, `GameVault.Controllers`)
- **Separate concerns**: Keep models, controllers, and components in their respective folders

#### API Controllers
- Always inherit from `ControllerBase` for API controllers
- Use `[ApiController]` attribute for automatic model validation
- Use `[Route("api/[controller]")]` for RESTful routing
- Include XML documentation comments on all public methods
- Use `[ProducesResponseType]` attributes to document possible HTTP responses
- **Use dependency injection**: Inject services through constructor, never use static methods
- Return appropriate HTTP status codes:
  - `200 OK` - Successful GET
  - `201 Created` - Successful POST with resource creation
  - `204 No Content` - Successful PUT/DELETE
  - `400 Bad Request` - Validation errors
  - `404 Not Found` - Resource not found

#### Models
- Place all data models in the `Models/` directory
- Use XML documentation comments for all public properties
- Use `required` keyword for mandatory properties (C# 14)
- Follow Pascal case naming conventions

#### Dependency Injection
- **Always use dependency injection** for business logic and data access
- Create interface and implementation for each service
- Interface naming: Use `I` prefix (e.g., `IGameService`, `IGameRepository`)
- Place interfaces and implementations in `Services/` folder
- **Register all services in Program.cs** immediately after creating them
- Service lifetime guidelines:
  - **Scoped**: Default choice for most services (per-request lifetime)
  - **Singleton**: For stateless services or application-wide state
  - **Transient**: For lightweight, stateless services called frequently
- Example pattern:
  ```csharp
  // Interface: Services/IGameService.cs
  public interface IGameService
  {
      Task<IEnumerable<Game>> GetAllGamesAsync();
  }
  
  // Implementation: Services/GameService.cs
  public class GameService : IGameService
  {
      public async Task<IEnumerable<Game>> GetAllGamesAsync()
      {
          // Implementation
      }
  }
  
  // Registration in Program.cs
  builder.Services.AddScoped<IGameService, GameService>();
  ```

#### Documentation
- Add XML summary comments to:
  - All public classes
  - All public methods
  - All public properties
  - Controller action parameters
- Be descriptive but concise in documentation

#### Blazor Components
- Prioritize Blazor Server solutions over Razor Pages or MVC
- Use Interactive Server render mode for interactive components
- Follow Blazor naming conventions (Pascal case for component files)
- Use `@inject` directive for dependency injection in components
- Always use the `@code` block for C# logic

### API Development Guidelines

#### RESTful Conventions
- Use proper HTTP verbs: GET, POST, PUT, DELETE
- Follow RESTful URL patterns: `/api/games`, `/api/games/{id}`
- Return meaningful error messages with proper structure: `new { message = "..." }`

#### Validation
- Validate required fields before processing
- Return `400 Bad Request` with clear error messages for validation failures
- Check for null/empty strings using `string.IsNullOrWhiteSpace()`

#### Response Patterns
```csharp
// Success with data
return Ok(data);

// Created resource
return CreatedAtAction(nameof(GetResource), new { id = resource.Id }, resource);

// No content (successful update/delete)
return NoContent();

// Not found
return NotFound(new { message = "Resource not found." });

// Bad request
return BadRequest(new { message = "Validation error." });
```

### OpenAPI/Scalar Configuration
- OpenAPI is configured in `Program.cs` with `AddOpenApi()` and `MapOpenApi()`
- Scalar UI is available at `/scalar/v1` for API documentation
- Ensure all API endpoints are properly documented with XML comments and `[ProducesResponseType]` attributes

### Current Features
- **Games API**: Full CRUD operations for managing video game entries
  - GET /api/games - List all games
  - GET /api/games/{id} - Get single game
  - POST /api/games - Create new game
  - PUT /api/games/{id} - Update existing game
  - DELETE /api/games/{id} - Delete game
- **In-memory storage**: Using static list for testing (data resets on app restart)

## When Creating New Features

### Adding a New API Controller
1. Create controller in `Controllers/` folder
2. Inherit from `ControllerBase`
3. Add `[ApiController]` and `[Route("api/[controller]")]` attributes
4. Include XML documentation on all methods
5. Add `[ProducesResponseType]` attributes for all possible responses
6. Use appropriate HTTP verbs and status codes
7. **Inject required services via constructor**

### Adding a New Service
1. Create interface in `Services/` folder with `I` prefix
2. Create implementation class in `Services/` folder
3. Add XML documentation to interface and implementation
4. **Register service in Program.cs** with appropriate lifetime
5. Use constructor injection in controllers/components

### Adding a New Model
1. Create model file in `Models/` folder
2. Add XML documentation to class and all properties
3. Use `required` keyword for mandatory properties
4. Follow the namespace pattern: `GameVault.Models`

### Adding a New Blazor Component
1. Create component in `Components/` folder (or appropriate subfolder)
2. Use `.razor` extension for component files
3. Follow Blazor component conventions
4. Use Interactive Server render mode when needed
5. Always use the `@code` block for C# logic
6. Use `@inject` for dependency injection

## Development Workflow

### **CRITICAL: Always Present Plan First**
When implementing new features or making significant changes:

1. **ALWAYS show your plan first** before writing any code
2. Explain:
   - What files will be created or modified
   - What changes will be made and why
   - What services need to be registered
   - How the changes fit into the existing architecture
3. **Wait for user confirmation** before proceeding with implementation
4. Only after receiving approval, proceed with the actual code changes

This ensures alignment on approach and prevents unnecessary rework.

## NuGet Packages in Use
- `Microsoft.AspNetCore.OpenApi` - OpenAPI specification generation
- `Scalar.AspNetCore` - Modern API documentation UI

## General Guidance
- **Follow existing patterns**: Look at `GamesController.cs` and `Game.cs` as reference implementations
- **Use dependency injection**: Never use static classes for business logic
- **Always register services**: Any new service must be registered in Program.cs
- **Present your plan first**: Show what you'll do before implementing
- **Keep it clean**: Prefer readability over cleverness
- **Be consistent**: Match the coding style of existing files
- **Document everything**: XML comments are not optional
- **Validate inputs**: Always check user input before processing
- **Use modern C# features**: Leverage C# 14 and .NET 10 features appropriately
- **Follow best practices**: SOLID principles, separation of concerns, testability
