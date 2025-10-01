# Asset Management Application

## 📄 Overview

This is an Asset Management Application developed using **ASP.NET Blazor WebAssembly** (Client) and **Web API** (Server), adhering to a clean **Layered Architecture** (Services/BLL, Data Access/DAL). The application currently features minimal UI elements, focusing primarily on robust architecture and data access strategies.

The application provides administrative functionality to manage employees, maintain a central asset inventory, and track asset assignments. It is built to meet high standards for code separation, data access optimization, and secure configuration.

**Key Technologies:**
* **Frontend/Backend:** ASP.NET Core Blazor (WebAssembly Client & Web API Server)
* **Data Access:** **Entity Framework Core** (CRUD/Transactions) and **Dapper** (Reporting/Performance Queries)
* **Database:** Microsoft SQL Server
* **Authentication:** JWT-based Token Authentication

***

## 🔐 Admin Login Credentials

The application uses pre-configured credentials for administrative access, which are securely read from the server's `appsettings.json` file via `IConfiguration`.

| Field | Value |
| :--- | :--- |
| **Username** | `admin` |
| **Password** | `password123` |

***

## ⚙️ Setup and Installation Instructions

### Prerequisites

1.  **.NET 8.0 SDK** (or later)
2.  **Microsoft SQL Server** (LocalDB, Express, or full instance)
3.  **Visual Studio 2022** (Recommended for solution and project management)

### Step 0: Project Setup and Dependencies 🛠️

1.  **Open Terminal:** Navigate to the solution's root directory.
2.  **Restore Dependencies:** Run the following command to download all required NuGet packages (as defined in the `.csproj` files):

    ```bash
    dotnet restore
    ```
3.  **Build Check:** Run a build to verify the project structure:

    ```bash
    dotnet build
    ```

### Step 1: Database Configuration 💾

1.  **Update Connection String:** Open the file `AssetManagement.Server/appsettings.json`.
2.  Locate the `"ConnectionStrings"` section and update the `DefaultConnection` to point to your local SQL Server instance.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AssetManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```
    *(Note: The example above uses SQL Server LocalDB.)*

3.  **Run EF Core Migrations and Seed Data:**
    Open the **Package Manager Console** in Visual Studio. Ensure the **Default project** is set to `AssetManagement.Server`. Run the following command:

    ```bash
    Update-Database
    ```
    This command creates the required schema and runs the integrated data seeding logic for initial testing/demo purposes.

### Step 2: Running the Application ▶️

The solution requires both the Client (UI) and Server (API) projects to run simultaneously. Choose one of the two methods below:

#### Option A: Running via Visual Studio (Recommended for Debugging)

1.  **Set Startup Projects:** In Visual Studio, right-click the solution file, select **"Set Startup Projects..."**.
2.  Choose **"Multiple startup projects"**.
3.  Set the **Action** for both the Client project (`AssetManagementApp`) and the Server project (`AssetManagement.Server`) to **"Start"**.
4.  Press **F5** or the **Run** button.

The application will launch in your browser, directing you to the login page.

#### Option B: Running via Separate Terminals (CLI)

1.  **Open two separate terminals** (e.g., Command Prompt, PowerShell, or Git Bash) and navigate to the **solution root directory**.
2.  **Start the API Server:** In the **first terminal**, navigate to the **Server project directory** (`AssetManagement.Server/`) and run:
    ```bash
    dotnet run
    ```
    *The server will start listening on **`http://localhost:5083`**.*
3.  **Start the Blazor Client:** In the **second terminal**, navigate to the **Project directory** ( `AssetManagementApp/`) and run:
    ```bash
    dotnet run
    ```
    *The client will launch a browser instance, typically pointing to **`http://localhost:5153`** (which hosts the WebAssembly app and proxies requests to the server running on port 5083).*
***

## 🧱 Architecture and Data Access Strategy

The solution structure enforces a clean separation of concerns, ensuring maintainability and scalability.

| Layer | Project | Responsibility | Data Access Tools |
| :--- | :--- | :--- | :--- |
| **UI Layer** | `AssetManagement.Client` | User interface and communication with the API. | API Clients |
| **Service (BLL)** | `AssetManagement.Application` | All business logic, validation, and complex transaction handling. | DAL |
| **Data Access (DAL)** | `AssetManagement.Infrastructure` | Direct database interaction and object mapping. | **EF Core (CRUD)**, **Dapper (Reads/Reports)** |
| **API/Host** | `AssetManagement.Server` | Hosts the Web API endpoints, handles JWT authentication, and manages Dependency Injection. | Service (BLL) |

### Dual Data Access Strategy

* **Entity Framework Core:** Used exclusively for **all standard CRUD operations and transactional changes** to leverage its change tracking and unit-of-work features.
* **Dapper:** Used specifically for **performance-sensitive queries and reporting** to minimize overhead and maximize read speed.

***

## ☑️ Demo Requirements Checklist

| Requirement | Status |
| :--- | :--- |
| Seed sample data for testing/demo | ☑️ |
| Desktop-focused responsive UI | ✖️ |
| Error handling and input validation | ☑️ |
| SQL Server DB setup via EF Core migrations | ☑️ |
| Admin login credentials included in documentation | ☑️ |
| Use Entity Framework Core for all standard CRUD and transactions | ☑️ |
| Use Dapper for performance-sensitive queries and reporting | ☑️ |
| Follow a clean layered architecture | ☑️ |
