# VibeTools - AI Tools Review Platform

A full-stack application for discovering and reviewing AI tools, built with .NET Core Web API backend and Next.js React frontend.

## 🚀 Features

- **Browse AI Tools**: Discover tools across categories (AI Assistant, IDE Extension, IDE, Creative, Productivity)  
- **Search & Filter**: Find tools by name, description, or category
- **Tool Reviews**: Read and write reviews with star ratings
- **Community Favorites**: See tools marked as community favorites
- **Submit Tools**: Add new tools to the platform
- **Power Rangers Reviews**: Seeded with fun reviews from Power Rangers characters!

## 🛠️ Tech Stack

### Backend (.NET Core Web API)
- ASP.NET Core 8.0
- Entity Framework Core with SQL Server
- Repository Pattern
- Service Layer Architecture
- Swagger/OpenAPI Documentation
- Database Migrations

### Frontend (Next.js React)
- Next.js 15.3.4 with App Router
- React 19
- TypeScript
- CSS Modules (Custom Styling)
- Lucide React Icons

## 📁 Project Structure

```
vibe-tools/
├── VibeTools/                    # .NET Core Web API
│   ├── Controllers/
│   ├── Models/
│   │   ├── Entities/
│   │   └── DTOs/
│   ├── Data/
│   ├── Services/
│   ├── Repositories/
│   └── Extensions/
├── frontend/
│   └── vibe-tools/               # Next.js React App
│       ├── src/
│       │   ├── app/
│       │   ├── components/
│       │   ├── services/
│       │   └── types/
│       └── public/
└── README.md
```

## 🏃‍♂️ Running the Application

### Prerequisites
- .NET 8.0 SDK
- Node.js 18+ 
- npm or yarn
- SQL Server (Docker recommended - see [SQL Server Setup Guide](SQL_SERVER_SETUP.md))

### Database Setup

1. **Option 1: Docker (Recommended)**
   ```bash
   # Start SQL Server container
   docker compose up -d
   ```

2. **Option 2: Local SQL Server**
   - Install SQL Server locally
   - Update connection string in `appsettings.Development.json`

3. **See detailed setup instructions:** [SQL_SERVER_SETUP.md](SQL_SERVER_SETUP.md)

### Backend (.NET API)

1. Navigate to the backend directory:
   ```bash
   cd VibeTools
   ```

2. Restore packages and run:
   ```bash
   dotnet restore
   dotnet run
   ```

3. The application will automatically:
   - Apply database migrations
   - Create tables
   - Seed sample data

4. API will be available at:
   - HTTPS: `https://localhost:5001`
   - HTTP: `http://localhost:5000`
   - Swagger UI: `https://localhost:5001/swagger`

### Frontend (Next.js)

1. Navigate to the frontend directory:
   ```bash
   cd frontend/vibe-tools
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Run the development server:
   ```bash
   npm run dev
   ```

4. Frontend will be available at: `http://localhost:3000`

## 🔗 API Endpoints

- `GET /api/tools` - Get all tools (with optional search)
- `GET /api/tools/{id}` - Get tool by ID
- `POST /api/tools` - Create new tool
- `PUT /api/tools/{id}` - Update tool
- `DELETE /api/tools/{id}` - Delete tool
- `GET /api/tools/{id}/reviews` - Get reviews for a tool
- `POST /api/tools/{id}/reviews` - Add review to a tool

## 🌱 Sample Data

The application comes pre-seeded with:
- **30 AI Tools** across 5 categories
- **28 Reviews** from Power Rangers characters
- Categories: AI Assistant, IDE Extension, IDE, Creative, Productivity

## 🔧 Configuration

### Frontend Environment Variables
Create a `.env.local` file in the frontend directory:

```env
NEXT_PUBLIC_API_URL=https://localhost:5001/api
NEXT_PUBLIC_DATA_SOURCE=mock
```

### Backend CORS Configuration
The API is configured to allow requests from `http://localhost:3000` and `https://localhost:3000`.

## 🧪 Development Notes

- The frontend uses CSS Modules for styling (Tailwind CSS removed)
- To connect frontend to the real API, ensure both backend and frontend are running
- The API uses SQL Server database with Entity Framework migrations
- Database is automatically created and seeded on first run
- All reviews include fun Power Rangers character names and themed comments

## 🗄️ Database

- **Database**: SQL Server (Docker recommended)
- **ORM**: Entity Framework Core 9.0
- **Migrations**: Automatic on startup
- **Seeding**: 30 tools + 28 Power Rangers reviews

### Database Commands
```bash
# Create migration
dotnet ef migrations add MigrationName

# Apply migrations manually
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## 🚀 Future Enhancements

- [ ] User authentication and authorization
- [ ] Azure deployment with Azure SQL Database
- [ ] Image uploads for tools
- [ ] Advanced filtering and sorting
- [ ] User profiles and review history
- [ ] Email notifications for new reviews
- [ ] API rate limiting
- [ ] Real-time updates with SignalR

## 📝 License

This project is licensed under the MIT License.
