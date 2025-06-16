# Reqres API Integration (.NET 7 - Clean Architecture)

This project is a .NET 7 class library and console application that demonstrates integration with the public [Reqres API](https://reqres.in/) to fetch user information using **HttpClient**, with support for **Clean Architecture**, **in-memory caching**, **error handling**, **retry logic**, and the **Options pattern**.

---

## 🚀 Features

* 🔌 API Client using `HttpClient` with `IHttpClientFactory`
* 📦 Strongly typed models (DTOs) with JSON mapping
* 🧱 Clean Architecture structure (Domain, Application, Infrastructure, Presentation)
* ♻️ In-Memory Caching with configurable expiration
* 🔁 Retry logic for transient failures using Polly
* ⚙️ Options Pattern to configure base URL and headers
* 🧪 Unit Tests for services
* 📺 Console App demonstrating usage

---

## 📂 Project Structure

```
/src
│
├── Domain/                  
├── Application/             
├── Infrastructure/         
├── Presentation/            
└── Tests/                   
```

---

## ⚙️ Configuration

`appsettings.json` or options pattern is used to configure:

```json
{
  "ReqresApiOptions": {
    "BaseUrl": "https://reqres.in/api/",
    "ApiKey": "reqres-free-v1",
    "CacheDurationInMinutes": 5
  }
}
```

Registered via:

```csharp
services.Configure<ReqresApiOptions>(configuration.GetSection("ReqresApiOptions"));
services.AddHttpClient<IUserService, ExternalUserService>()
        .AddPolicyHandler(...) // Polly retry logic
```

---

## 💠 How to Run

1. **Clone the repository**

```bash
git clone https://github.com/your-username/reqres-api-integration.git
cd reqres-api-integration
```

2. **Build the project**

```bash
dotnet build
```

3. **Run the console application**

```bash
dotnet run --project src/Presentation
```

---

## 🧪 How to Test

Run unit tests from the test project:

```bash
dotnet test
```

---

## 💡 Example Usage

```csharp
var user = await _userService.GetUserByIdAsync(2);
Console.WriteLine($"{user.FirstName} {user.LastName} - {user.Email}");

var allUsers = await _userService.GetAllUsersAsync();
```

---

## 🧠 Design Decisions

* **Clean Architecture**: Separation of concerns between layers improves maintainability and testability.
* **Polly**: To handle transient faults like network issues.
* **IMemoryCache**: Improves performance by reducing repeated API calls.
* **Options Pattern**: Allows flexible configuration of API endpoints and settings.

## 📸 Screenshots 
![image](https://github.com/user-attachments/assets/20377c89-5251-493d-8fa2-eea12e297e1d)
![image](https://github.com/user-attachments/assets/f82155b3-7dbc-4a7a-948d-511907d67173)
![image](https://github.com/user-attachments/assets/f8a322b0-7712-49b5-b1dd-b393b2c2a2fd)
![image](https://github.com/user-attachments/assets/c9e1f501-4f39-408d-8892-c3e198451d22)



---


## 📄 License

MIT License
