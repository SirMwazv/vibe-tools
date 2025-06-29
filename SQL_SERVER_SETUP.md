# SQL Server Setup Guide for VibeTools

This guide will help you set up SQL Server for your VibeTools application.

## Option 1: Docker (Recommended)

### Prerequisites
- Docker Desktop installed and running on your Mac

### Steps

1. **Start SQL Server container:**
   ```bash
   cd /Users/mwazvitamutowo/RiderProjects/vibe-tools
   docker compose up -d
   ```

2. **Verify SQL Server is running:**
   ```bash
   docker compose ps
   ```

3. **Run the application:**
   ```bash
   cd VibeTools
   dotnet run
   ```

The application will automatically:
- Apply database migrations
- Create the database tables
- Seed initial data

## Option 2: Local SQL Server Installation

### Prerequisites
- SQL Server installed locally
- SQL Server running on localhost:1433

### Steps

1. **Update connection string in appsettings.Development.json if needed:**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost,1433;Database=VibeToolsDb_Dev;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;MultipleActiveResultSets=True"
     }
   }
   ```

2. **Run the application:**
   ```bash
   cd VibeTools
   dotnet run
   ```

## Option 3: Azure SQL Database

### Prerequisites
- Azure account
- Azure SQL Database created

### Steps

1. **Update connection string in appsettings.json:**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=tcp:your-server.database.windows.net,1433;Database=VibeToolsDb;User Id=your-username;Password=your-password;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
     }
   }
   ```

2. **Run the application:**
   ```bash
   cd VibeTools
   dotnet run
   ```

## Database Commands

### Create a new migration:
```bash
cd VibeTools
export PATH="$PATH:/Users/mwazvitamutowo/.dotnet/tools"
dotnet ef migrations add MigrationName
```

### Apply migrations manually:
```bash
cd VibeTools
export PATH="$PATH:/Users/mwazvitamutowo/.dotnet/tools"
dotnet ef database update
```

### Remove last migration:
```bash
cd VibeTools
export PATH="$PATH:/Users/mwazvitamutowo/.dotnet/tools"
dotnet ef migrations remove
```

## Connection String Explanation

The connection string includes:
- **Server**: SQL Server instance location
- **Database**: Database name (VibeToolsDb_Dev for development)
- **User Id/Password**: SQL Server authentication
- **TrustServerCertificate=True**: Required for local development
- **MultipleActiveResultSets=True**: Allows multiple queries simultaneously

## Security Notes

1. **Never commit passwords to source control**
2. **Use different passwords for development and production**
3. **Consider using Azure Key Vault for production secrets**
4. **Use Managed Identity when deploying to Azure**

## Troubleshooting

### Connection Issues:
1. Verify SQL Server is running
2. Check firewall settings
3. Confirm connection string is correct
4. Test connection with SQL Server Management Studio or Azure Data Studio

### Migration Issues:
1. Ensure EF Core tools are installed: `dotnet tool install --global dotnet-ef`
2. Add tools to PATH: `export PATH="$PATH:/Users/mwazvitamutowo/.dotnet/tools"`
3. Check for compilation errors in your DbContext

### Docker Issues:
1. Ensure Docker Desktop is running
2. Check container logs: `docker compose logs sqlserver`
3. Restart container: `docker compose restart sqlserver`
