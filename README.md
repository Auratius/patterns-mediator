# Patterns Mediator

A C# implementation of the Mediator Pattern that provides a clean, decoupled way to handle communication between objects in your application.

## Project Overview

The **Mediator Pattern** is a behavioral design pattern that defines how objects interact with each other. Instead of objects communicating directly, they communicate through a central mediator object. This pattern promotes loose coupling by keeping objects from referring to each other explicitly, and it lets you vary their interaction independently.

### Key Benefits

- **Decoupling**: Components don't need to know about each other directly
- **Centralized Control**: All communication flows through a single mediator
- **Reusability**: Handlers can be reused across different parts of the application
- **Testability**: Easy to mock and test individual components

## Features

This implementation provides:

- **Generic Request/Response Pattern**: Type-safe request and response handling
- **Handler Registration System**: Dynamic registration of request handlers
- **Type-Safe Dispatching**: Compile-time type checking for request/response pairs
- **Windows Service Integration**: Built-in support for running as a Windows service
- **Extensible Architecture**: Easy to add new request types and handlers
- **Minimal Dependencies**: Lightweight implementation with no external dependencies

## Usage

### Basic Setup

```csharp
// Create a mediator instance
var mediator = new Mediator();

// Register handlers
mediator.RegisterHandler(new PingHandler());

// Send requests
var request = new PingRequest();
var response = mediator.Send(request);
Console.WriteLine(response); // Output: "Pong"
```

### Creating Custom Requests and Handlers

1. **Define a Request**:
```csharp
public class CalculateRequest : IRequest<int>
{
    public int A { get; set; }
    public int B { get; set; }
}
```

2. **Create a Handler**:
```csharp
public class CalculateHandler : IRequestHandler<CalculateRequest, int>
{
    public int Handle(CalculateRequest request)
    {
        return request.A + request.B;
    }
}
```

3. **Register and Use**:
```csharp
var mediator = new Mediator();
mediator.RegisterHandler(new CalculateHandler());

var request = new CalculateRequest { A = 5, B = 3 };
var result = mediator.Send(request);
Console.WriteLine(result); // Output: 8
```

### Windows Service Usage

The implementation includes a Windows service wrapper:

```csharp
static void Main()
{
    var mediator = new Mediator();
    mediator.RegisterHandler(new PingHandler());
    ServiceBase.Run(new MediatorService(mediator));
}
```

## Installation and Build

### Prerequisites

- .NET Framework or .NET Core/5+
- Visual Studio or VS Code (recommended)

### Build Instructions

**Note**: The project currently lacks a `.csproj` file. You'll need to create one before building.

1. **Clone the repository**:
```bash
git clone https://github.com/Auratius/patterns-mediator.git
cd patterns-mediator
```

2. **Create a project file** (if missing):
```bash
# Navigate to the src directory
cd src

# Create a new console project
dotnet new console -n PatternsMediator

# Add Windows service support (if needed)
dotnet add package Microsoft.Extensions.Hosting.WindowsServices
```

3. **Build the solution**:
```bash
# From the root directory
dotnet build
```

4. **Run the application**:
```bash
dotnet run --project src
```

### Project Structure

```
patterns-mediator/
├── src/
│   ├── IMediator.cs          # Core mediator interface
│   ├── Mediator.cs           # Main mediator implementation
│   ├── IRequest.cs           # Request interface
│   ├── IRequestHandler.cs    # Handler interface
│   ├── PingRequest.cs        # Example request/handler
│   ├── MediatorService.cs    # Windows service wrapper
│   └── Program.cs            # Application entry point
├── PatternsMediator.sln      # Visual Studio solution file
└── README.md                 # This file
```

## License

This project is licensed under the MIT License - see below for details:

```
MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## Contact and Contribution

### Contributing

We welcome contributions! Please follow these guidelines:

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/your-feature-name`
3. **Make your changes** and add tests if applicable
4. **Ensure all tests pass** and the code builds successfully
5. **Submit a pull request** with a clear description of your changes

### Code Style

- Follow standard C# naming conventions
- Use meaningful variable and method names
- Include XML documentation comments for public APIs
- Keep methods focused and single-purpose

### Reporting Issues

If you encounter any bugs or have feature requests, please:

1. Check existing issues first
2. Create a new issue with a clear description
3. Include steps to reproduce (for bugs)
4. Provide relevant code samples when possible

### Contact

For questions or discussions about this implementation:

- **GitHub Issues**: Use the repository's issue tracker
- **Pull Requests**: For code contributions and improvements

---

*This implementation demonstrates a clean, extensible approach to the Mediator Pattern in C#. Feel free to use it as a foundation for your own projects or as a learning resource.*