# HR Leave Management System

A comprehensive HR Leave Management System built with **ASP.NET Core** following **SOLID principles** and **Clean Architecture** patterns.

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
  - [Running the Application](#running-the-application)
- [Features](#features)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

## 🎯 Overview

This HR Leave Management System is a robust solution for managing employee leave requests, leave allocations, and leave types. The application is designed with scalability, maintainability, and testability in mind, leveraging Clean Architecture principles to separate concerns and dependencies.

## 🏗️ Architecture

The solution follows **Clean Architecture** with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│            (API, Blazor UI, Controllers)                 │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│                  Application Layer                       │
│        (Business Logic, CQRS, MediatR, DTOs)            │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│                   Domain Layer                           │
│            (Entities, Business Rules)                    │
└─────────────────────────────────────────────────────────┘
           │                            │
┌──────────▼────────────┐    ┌─────────▼────────────────┐
│ Infrastructure Layer  │    │   Persistence Layer      │
│  (Email, Logging)     │    │ (EF Core, Repositories)  │
└───────────────────────┘    └──────────────────────────┘
```

### Key Architectural Patterns

- **Clean Architecture**: Clear separation between Domain, Application, Infrastructure, and Presentation layers
- **CQRS (Command Query Responsibility Segregation)**: Using MediatR for command and query handling
- **Repository Pattern**: Abstraction layer for data access
- **Dependency Injection**: Loose coupling between components
- **Unit of Work**: Transaction management across repositories

## 🛠️ Technologies

- **Framework**: .NET 8.0 / ASP.NET Core
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **API Documentation**: Swagger/OpenAPI
- **Object Mapping**: AutoMapper
- **Mediator Pattern**: MediatR
- **Frontend**: Blazor WebAssembly
- **Testing**: xUnit, Moq
- **Logging**: Custom logging infrastructure
- **Email**: SMTP Email Service

## 📁 Project Structure

```
HR.LeaveManagement.Clean/
├── HR.LeaveManagement.Api/                    # Web API Project
│   ├── Controllers/                            # API Controllers
│   │   ├── LeaveAllocationsController.cs
│   │   ├── LeaveRequestsController.cs
│   │   └── LeaveTypesController.cs
│   ├── Middleware/                             # Custom Middleware
│   │   └── ExceptionMiddleware.cs
│   └── Program.cs                              # Application Entry Point
│
├── HR.LeaveManagement.Application/             # Application Layer
│   ├── Features/                               # CQRS Features
│   │   ├── LeaveAllocation/                    # Leave Allocation Features
│   │   ├── LeaveRequest/                       # Leave Request Features
│   │   └── LeaveType/                          # Leave Type Features
│   ├── Contracts/                              # Interfaces
│   │   ├── Email/                              # Email Service Contracts
│   │   ├── Logging/                            # Logging Contracts
│   │   └── Persistence/                        # Repository Contracts
│   ├── Exceptions/                             # Custom Exceptions
│   ├── MappingProfiles/                        # AutoMapper Profiles
│   └── Models/                                 # DTOs and View Models
│
├── HR.LeaveManagement.Domain/                  # Domain Layer
│   ├── LeaveAllocation.cs                      # Leave Allocation Entity
│   ├── LeaveRequest.cs                         # Leave Request Entity
│   ├── LeaveType.cs                            # Leave Type Entity
│   └── Common/                                 # Base Entities
│
├── HR.LeaveManagement.Persistence/             # Data Access Layer
│   ├── DatabaseContext/                        # DbContext
│   ├── Repositories/                           # Repository Implementations
│   ├── Configurations/                         # Entity Configurations
│   └── Migrations/                             # EF Core Migrations
│
├── HR.LeaveManagement.Infrastructure/          # Infrastructure Layer
│   ├── EmailService/                           # Email Implementation
│   └── Logging/                                # Logging Implementation
│
├── HR.LeaveManagement.BlazorUI/                # Blazor Frontend
│   ├── Pages/                                  # Razor Pages
│   ├── Services/                               # Frontend Services
│   └── Models/                                 # Frontend Models
│
├── HR.LeaveManagement.Application.UnitTests/   # Unit Tests
└── HR.LeaveManagement.Persistence.IntegrationTests/ # Integration Tests
```

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express, Developer, or higher)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (optional, for database management)

### Installation

1. **Clone the repository**

   ```powershell
   git clone https://github.com/agurokeendavid/HR.LeaveManagement.Clean.git
   cd HR.LeaveManagement.Clean
   ```

2. **Restore NuGet packages**

   ```powershell
   dotnet restore
   ```

3. **Build the solution**

   ```powershell
   dotnet build
   ```

### Database Setup

#### Option 1: Using Existing Migrations

1. **Update the connection string**

   Open `HR.LeaveManagement.Api/appsettings.Development.json` and update the connection string:

   ```json
   {
     "ConnectionStrings": {
       "HrDatabaseConnectionString": "Server=YOUR_SERVER_NAME;Database=HRLeaveManagement;Integrated Security=True;TrustServerCertificate=True"
     }
   }
   ```

   Replace `YOUR_SERVER_NAME` with:
   - `.` or `localhost` for local SQL Server
   - `.\SQLEXPRESS` for SQL Server Express
   - Your actual server name for remote instances

2. **Apply migrations to create the database**

   Navigate to the API project directory:

   ```powershell
   cd HR.LeaveManagement.Api
   ```

   Run the migration command:

   ```powershell
   dotnet ef database update --project ..\HR.LeaveManagement.Persistence\HR.LeaveManagement.Persistence.csproj
   ```

   Or from the solution root:

   ```powershell
   dotnet ef database update --project HR.LeaveManagement.Persistence --startup-project HR.LeaveManagement.Api
   ```

#### Option 2: Creating New Migrations

If you need to create new migrations after modifying entities:

1. **Add a new migration**

   ```powershell
   dotnet ef migrations add YourMigrationName --project HR.LeaveManagement.Persistence --startup-project HR.LeaveManagement.Api
   ```

2. **Update the database**

   ```powershell
   dotnet ef database update --project HR.LeaveManagement.Persistence --startup-project HR.LeaveManagement.Api
   ```

3. **Remove last migration** (if needed)

   ```powershell
   dotnet ef migrations remove --project HR.LeaveManagement.Persistence --startup-project HR.LeaveManagement.Api
   ```

### Running the Application

#### Running the API

1. **Navigate to the API project**

   ```powershell
   cd HR.LeaveManagement.Api
   ```

2. **Run the application**

   ```powershell
   dotnet run
   ```

   Or for hot reload during development:

   ```powershell
   dotnet watch run
   ```

3. **Access the API**

   The API will be available at:
   - HTTPS: `https://localhost:7209` (check your `launchSettings.json` for actual port)
   - HTTP: `http://localhost:5209`
   - Swagger UI: `https://localhost:7209/swagger`

#### Running the Blazor UI

1. **Navigate to the Blazor project**

   ```powershell
   cd HR.LeaveManagement.BlazorUI
   ```

2. **Run the application**

   ```powershell
   dotnet run
   ```

3. **Access the application**

   Open your browser and navigate to the URL displayed in the console (typically `https://localhost:7xxx`)

#### Running Both Projects Simultaneously

You can configure multiple startup projects in Visual Studio:

1. Right-click on the solution in Solution Explorer
2. Select "Configure Startup Projects"
3. Choose "Multiple startup projects"
4. Set both `HR.LeaveManagement.Api` and `HR.LeaveManagement.BlazorUI` to "Start"

## ✨ Features

### Core Functionality

- **Leave Type Management**
  - Create, read, update, and delete leave types
  - Define default days for each leave type

- **Leave Allocation Management**
  - Allocate leave days to employees
  - Track remaining leave balance
  - Manage allocation periods

- **Leave Request Management**
  - Submit leave requests
  - Approve/reject leave requests
  - Track leave request status
  - View leave request history

### Technical Features

- **RESTful API** with comprehensive endpoints
- **Global Exception Handling** middleware
- **Swagger Documentation** for easy API exploration
- **CORS Support** for frontend integration
- **Entity Framework Core** with Code-First approach
- **Automated Mapping** with AutoMapper
- **CQRS Pattern** for clean command/query separation

## 🔌 API Endpoints

### Leave Types

```
GET    /api/leavetypes          - Get all leave types
GET    /api/leavetypes/{id}     - Get leave type by ID
POST   /api/leavetypes          - Create new leave type
PUT    /api/leavetypes/{id}     - Update leave type
DELETE /api/leavetypes/{id}     - Delete leave type
```

### Leave Allocations

```
GET    /api/leaveallocations       - Get all leave allocations
GET    /api/leaveallocations/{id}  - Get leave allocation by ID
POST   /api/leaveallocations       - Create new leave allocation
PUT    /api/leaveallocations/{id}  - Update leave allocation
DELETE /api/leaveallocations/{id}  - Delete leave allocation
```

### Leave Requests

```
GET    /api/leaverequests          - Get all leave requests
GET    /api/leaverequests/{id}     - Get leave request by ID
POST   /api/leaverequests          - Create new leave request
PUT    /api/leaverequests/{id}     - Update leave request
DELETE /api/leaverequests/{id}     - Delete leave request
```

For detailed API documentation, run the application and visit the Swagger UI at `/swagger`.

## 🧪 Testing

### Running Unit Tests

```powershell
# Run all tests
dotnet test

# Run tests for specific project
dotnet test HR.LeaveManagement.Application.UnitTests

# Run tests with coverage
dotnet test /p:CollectCoverage=true
```

### Running Integration Tests

```powershell
dotnet test HR.LeaveManagement.Persistence.IntegrationTests
```

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

Please ensure your code follows the existing architecture and coding standards.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 📞 Support

For questions or support, please open an issue in the GitHub repository.

## 🙏 Acknowledgments

- Built with Clean Architecture principles
- Inspired by industry best practices and SOLID principles
- Thanks to the .NET community for excellent tools and frameworks

---

**Happy Coding! 🚀** 
