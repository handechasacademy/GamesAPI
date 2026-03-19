# GamesAPI

A RESTful Web API built with ASP.NET Core 9 for managing a game library. Supports full CRUD, pagination, filtering, caching, JWT authentication, rate limiting, and integration with the RAWG game database.

---

## Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or VS Code

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-username/GamesAPI.git
cd GamesAPI
```

### 2. Set up User Secrets

This project uses User Secrets to store sensitive configuration. Run the following commands from the project folder (where the `.csproj` file is located):

```bash
dotnet user-secrets init
```

Then add the required secrets:

```bash
# JWT signing key (must be at least 16 characters)
dotnet user-secrets set "Jwt:Key" "YourSuperSecretKeyHere123!"

# Admin credentials for the login endpoint
dotnet user-secrets set "Admin:Username" "admin"
dotnet user-secrets set "Admin:Password" "yourpassword"

# RAWG API key (get yours free at https://rawg.io/apidocs)
dotnet user-secrets set "Rawg:ApiKey" "your-rawg-api-key"
```

### 3. Run the project

```bash
dotnet run
```

Or press **F5** in Visual Studio.

The API will be available at `https://localhost:7142` and Swagger UI at `https://localhost:7142/swagger`.

---

## Authentication

The API uses JWT Bearer authentication. Protected endpoints (POST, PUT, DELETE) require a valid token.

1. Go to `POST /api/Auth/login` in Swagger
2. Enter your admin credentials:
```json
{
  "username": "admin",
  "password": "yourpassword"
}
```
3. Copy the token from the response
4. Click the **Authorize** button in Swagger and paste the token

---

## Endpoints

| Resource | Base URL |
|---|---|
| Games | `/api/games` |
| Genres | `/api/genres` |
| Libraries | `/api/libraries` |
| Game Libraries | `/api/gamelibraries` |
| RAWG Search | `/api/rawg/search?name=` |
| Auth | `/api/Auth/login` |

All list endpoints support pagination via `?page=1&pageSize=20`.  
Games support filtering via `?genre=Action&playingType=0&searchTerm=zelda`.

---

## Configuration

Non-sensitive settings are in `appsettings.json`:

```json
{
  "Jwt": {
    "Issuer": "GamesAPI",
    "Audience": "GamesAPIClients"
  }
}
```

Sensitive settings (API keys, passwords) are stored in User Secrets, see setup instructions above.
