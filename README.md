# GamesAPI

A RESTful Web API built with ASP.NET Core 9 for managing a game library. You can create libraries, add games to them, mark favorites, and search for game info via the RAWG game database.

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

This project uses User Secrets to keep sensitive config out of the code. Run these commands from the folder where the `.csproj` file is:

```bash
dotnet user-secrets init
```

Then add the required secrets one by one:

```bash
# JWT signing key - needs to be at least 16 characters
dotnet user-secrets set "Jwt:Key" "YourSuperSecretKeyHere123!"

# Admin login credentials
dotnet user-secrets set "Admin:Username" "admin"
dotnet user-secrets set "Admin:Password" "yourpassword"

# RAWG API key - get yours free at https://rawg.io/apidocs
dotnet user-secrets set "Rawg:ApiKey" "your-rawg-api-key"
```

### 3. Run the project

```bash
dotnet run
```

Or just press **F5** in Visual Studio.

Swagger UI will be available at `https://localhost:7142/swagger`.

---

## How to authenticate

Most write operations (POST, PUT, DELETE) require a JWT token. Here's how to get one in Swagger:

1. Call `POST /api/Auth/login` with your admin credentials:
```json
{
  "username": "admin",
  "password": "yourpassword"
}
```
2. Copy the token from the response
3. Click the **Authorize** button at the top of Swagger
4. Paste the token and click Authorize

Now your requests will include the token automatically.

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

All list endpoints support pagination with `?page=1&pageSize=20`.

Games can be filtered with `?genre=Action&playingType=0&searchTerm=zelda`.

---

## Configuration

Non-sensitive settings live in `appsettings.json`:

```json
{
  "Jwt": {
    "Issuer": "GamesAPI",
    "Audience": "GamesAPIClients"
  }
}
```

Everything sensitive (API keys, passwords, JWT key) is stored in User Secrets as shown above.

---

## Performance — Cache Miss vs Cache Hit

I added HybridCache to the `GET /api/games/{id}` and `GET /api/libraries/{id}` endpoints to reduce unnecessary database calls.

I tested `GET /api/games/{id}` using the browser's Network tab in DevTools and got these results:

| Request | Time |
|---|---|
| First call (Cache Miss) | 652ms |
| Second call (Cache Hit) | 5ms |
| Third call (Cache Hit) | 3ms |
| Fourth call (Cache Hit) | 2ms |

The first call hits the database and takes around 652ms. After that the data is stored in memory, so the next calls come back in 2–5ms. That's roughly a 130x speedup just from caching!

The cache also gets cleared automatically when you update or delete a game, so you never get stale data back.
