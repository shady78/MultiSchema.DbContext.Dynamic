# MultiSchema.DbContext.Dynamic

A .NET solution for implementing dynamic schema switching in Entity Framework Core applications, perfect for multi-tenant architectures where schema isolation is required.

## 🌟 Features

- Dynamic schema switching at runtime
- Middleware support for schema resolution
- Action filters for schema validation
- Global parameter service for schema management
- Support for header-based and query parameter-based schema selection
- Thread-safe schema context handling

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 or later
- SQL Server (or compatible database)
- Entity Framework Core 8.0 or later

### Installation

1. Clone the repository:
```bash
git clone https://github.com/shady78/MultiSchema.DbContext.Dynamic.git
```

2. Add the required NuGet packages:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

3. Configure your connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your_Connection_String_Here"
  }
}
```

## 📋 Usage

### 1. Register Services

```csharp
builder.Services.AddScoped<IGlobalParameterService, GlobalParameterService>();
builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
    options.EnableServiceProviderCaching(false);
});
```

### 2. Configure Middleware

```csharp
app.UseMiddleware<GlobalParameterMiddleware>();
```

### 3. Use in Controllers

```csharp
[HttpGet]
public IActionResult GetAll([FromQuery] string schema)
{
    _parameterService.SetSchemaName(schema);
    using var scope = _serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    var results = context.YourEntity.ToList();
    return Ok(results);
}
```

## 🏗️ Architecture

The solution implements a three-layer architecture:

1. **Middleware Layer**: Handles schema resolution from HTTP headers
2. **Service Layer**: Manages schema state and validation
3. **Data Layer**: Implements dynamic schema switching in EF Core

## 🔑 Key Components

- `GlobalParameterMiddleware`: Extracts schema from request headers
- `GlobalParameterService`: Manages schema state
- `GlobalParameterActionFilter`: Validates schema parameters
- `ApplicationDbContext`: Implements dynamic schema switching

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## 🙏 Acknowledgments

- Entity Framework Core team for their amazing work
- The .NET community for their continuous support

## 📞 Contact

Contact Email: shadykhalifa.dotnetdev@gmail.com

Project Link: [https://github.com/shady78/MultiSchema.DbContext.Dynamic](https://github.com/shady78/MultiSchema.DbContext.Dynamic)

---
⭐️ If this project helped you, please consider giving it a star on GitHub!
