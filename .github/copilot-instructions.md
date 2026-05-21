# GitHub Copilot Instructions for This Project

## Project Context

This project is a modern ASP.NET Core Blazor Web App built using .NET and C#.

All generated code, suggestions, refactors, and architectural decisions should follow modern .NET best practices, clean architecture principles, and production-quality standards.

---

# General Development Standards

- Always follow Microsoft recommended best practices for ASP.NET Core and Blazor.
- Write clean, maintainable, readable, and production-ready code.
- Prefer clarity and maintainability over clever or overly compact code.
- Avoid code duplication.
- Favor composition over inheritance where appropriate.
- Use async/await properly for all I/O operations.
- Avoid blocking calls such as `.Result` or `.Wait()`.
- Use nullable reference types correctly.
- Use file-scoped namespaces.
- Use implicit usings where appropriate.
- Prefer constructor injection.
- Use dependency injection everywhere possible.
- Avoid static classes unless absolutely appropriate.
- Use meaningful naming conventions for classes, methods, variables, and properties.
- Keep methods small and focused on a single responsibility.
- Use `var` only when the type is obvious.
- Avoid magic strings and magic numbers.
- Prefer constants and strongly typed models.

---

# File Organization Rules

Always use separate files for all types.

Each of the following must be placed in its own individual file:

- Classes
- Interfaces
- Services
- DTOs
- ViewModels
- Enums
- Records
- Components
- Validators
- Extensions
- Middleware
- Exceptions

Never place multiple classes or interfaces in the same file.

Use clear folder organization.

Example structure:

```text
/Components
/Pages
/Services
/Services/Interfaces
/DTOs
/Models
/ViewModels
/Data
/Repositories
/Extensions
/Middleware
/Validators
/Configuration
/Exceptions