# Virtual Card Backend
This is the backend API for the Virtual Card system, built using C# and .NET. It provides secure and efficient endpoints for managing virtual card operations, including user authentication, card creation, transactions, and more.

# Tech Stack
- C#
- .NET API
- Entity Framework Core 
- SQL Server
- JWT Authentication(for hundling tokens)
- Setup & Installation
  
# Clone the Repository
- git clone https://github.com/Mikhabesha/virtual-card-backend.git
- cd virtual-card-backend
  
# Install Dependencies
- dotnet restore
  
# Set Up Database
- Configure your connection string in appsettings.json.
- Run migrations (if using EF Core):
  - dotnet ef database update
  - dotnet ef database update --project virtual-card-backend (if you need to specify the project)
# Run the API
- dotnet run
  
# API Documentation
- If using Swagger, open http://localhost:<port>/swagger to view available endpoints.
  
# Features
-  User authentication (JWT-based)✔️
-  Virtual card creation & management✔️
-  Transaction handling✔️ 
-  Secure RESTful API design✔️

