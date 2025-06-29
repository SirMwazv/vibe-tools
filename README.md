# VibeTools - AI Tools Review Platform

A full-stack application for discovering and reviewing AI tools, built with .NET Core Web API backend and Next.js React frontend. **Now fully Dockerized for easy deployment!**

## 🚀 Features

- **Browse AI Tools**: Discover tools across categories (AI Assistant, IDE Extension, IDE, Creative, Productivity)  
- **Search & Filter**: Find tools by name, description, or category
- **Tool Reviews**: Read and write reviews with star ratings
- **Community Favorites**: See tools marked as community favorites with golden borders
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

### Infrastructure
- Docker & Docker Compose
- SQL Server 2022 in Docker
- Multi-container orchestration
- Health checks and service dependencies

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
│   ├── Extensions/
│   └── Dockerfile               # Backend Docker config
├── frontend/
│   └── vibe-tools/               # Next.js React App
│       ├── src/
│       │   ├── app/
│       │   ├── components/
│       │   ├── services/
│       │   └── types/
│       ├── public/
│       └── Dockerfile           # Frontend Docker config
├── docker-compose.yml           # Full stack orchestration
├── manage-docker.sh            # Easy Docker management
└── README.md
```

## 🐳 Quick Start with Docker (Recommended)

### Prerequisites
- Docker Desktop (includes Docker Compose)
- Git (to clone the repository)

### One-Command Setup

1. **Clone and start everything:**
   ```bash
   git clone <repository-url>
   cd vibe-tools
   ./manage-docker.sh start
   ```

2. **Access the application:**
   - 🌐 **Frontend**: [http://localhost:3000](http://localhost:3000)
   - 🔗 **API**: [http://localhost:5140](http://localhost:5140)
   - 📖 **API Docs**: [http://localhost:5140/swagger](http://localhost:5140/swagger)
   - 🗄️ **Database**: localhost:1433 (sa / YourPassword123!)

That's it! The entire application is now running with sample data.

### Docker Management Commands

The `manage-docker.sh` script provides easy management:

```bash
# Start the entire application
./manage-docker.sh start

# Stop all services
./manage-docker.sh stop

# Restart everything
./manage-docker.sh restart

# View logs (all services)
./manage-docker.sh logs

# View logs for specific service
./manage-docker.sh logs api
./manage-docker.sh logs frontend
./manage-docker.sh logs sqlserver

# Check service status
./manage-docker.sh status

# Run database migrations
./manage-docker.sh migrate

# Seed database with fresh data
./manage-docker.sh seed

# Open shell in containers
./manage-docker.sh shell api
./manage-docker.sh shell frontend

# Clean up everything (removes all data)
./manage-docker.sh clean

# Show help
./manage-docker.sh help
```

### Manual Docker Commands

If you prefer using Docker Compose directly:

```bash
# Start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop all services
docker-compose down

# Rebuild and restart
docker-compose up --build -d

# Remove everything including volumes
docker-compose down -v --rmi all
```

## 🔧 Service Configuration

### Services Overview
- **sqlserver**: SQL Server 2022 database
- **api**: .NET Core Web API backend
- **frontend**: Next.js React application

### Network Configuration
All services run on a dedicated Docker network with the following ports:
- Frontend: Port 3000
- API: Port 5140 (mapped from container port 80)
- Database: Port 1433

### Environment Variables
The Docker setup includes pre-configured environment variables:
- Database connection strings
- API URLs
- CORS settings
- SQL Server authentication

## 🏃‍♂️ Local Development (Without Docker)

If you want to run components individually for development:

### Prerequisites
- .NET 8.0 SDK
- Node.js 18+ 
- npm or yarn
- SQL Server (Docker recommended)

### Database Setup

1. **Start only the database:**
   ```bash
   docker-compose up sqlserver -d
   ```

2. **Or use local SQL Server** and update connection strings

### Backend (.NET API)

```bash
cd VibeTools
dotnet restore
dotnet run
```

API available at: `https://localhost:5140` or `http://localhost:5140`

### Frontend (Next.js)

```bash
cd frontend/vibe-tools
npm install
npm run dev
```

Frontend available at: `http://localhost:3000`

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
- **Community Favorites** with golden borders and special badges
- Categories: AI Assistant, IDE Extension, IDE, Creative, Productivity

## �️ Database

- **Database**: SQL Server 2022 (Docker)
- **ORM**: Entity Framework Core 9.0
- **Migrations**: Automatic on startup
- **Seeding**: 30 tools + 28 Power Rangers reviews
- **Persistence**: Data persists in Docker volumes

### Database Access
- **Host**: localhost:1433
- **Username**: sa
- **Password**: YourPassword123!
- **Database**: VibeToolsDb

### Database Commands (in API container)
```bash
# Open shell in API container
./manage-docker.sh shell api

# Inside container
dotnet ef migrations add MigrationName
dotnet ef database update
dotnet ef migrations remove
```

## 🔐 Security Notes

### For Production Deployment:
1. **Change default passwords** in docker-compose.yml
2. **Use environment files** for sensitive configuration
3. **Enable HTTPS** with proper certificates
4. **Configure firewall rules** and network security
5. **Use secrets management** for production credentials
6. **Regular security updates** for base images

### Current Development Setup:
- Uses default SA password (change for production)
- HTTP connections (add HTTPS for production)
- Open CORS policy (restrict for production)

## 🚀 Deployment

### Local Production Build
```bash
# Build production images
docker-compose build

# Run in production mode
docker-compose up -d
```

### Cloud Deployment
The Docker setup is ready for deployment to:
- Azure Container Instances
- AWS ECS
- Google Cloud Run
- Kubernetes clusters
- Any Docker-compatible hosting

## 🧪 Troubleshooting

### Common Issues:

**"Port already in use" errors:**
```bash
# Stop any existing services
./manage-docker.sh stop
# Or kill processes using the ports
lsof -ti:3000,5140,1433 | xargs kill -9
```

**Database connection issues:**
```bash
# Check if SQL Server is healthy
docker-compose ps
./manage-docker.sh logs sqlserver
```

**Frontend API connection issues:**
- Ensure API is running and accessible at http://localhost:5140
- Check browser console for CORS errors
- Verify environment variables in docker-compose.yml

**Container build failures:**
```bash
# Clean Docker cache and rebuild
./manage-docker.sh clean
./manage-docker.sh start
```

### Logs and Monitoring:
```bash
# View all service logs
./manage-docker.sh logs

# Monitor specific service
./manage-docker.sh logs api -f

# Check container health
docker ps
docker-compose ps
```

## 🚀 Future Enhancements

- [ ] User authentication and authorization
- [ ] Azure deployment with Azure Container Instances
- [ ] Image uploads for tools
- [ ] Advanced filtering and sorting
- [ ] User profiles and review history
- [ ] Email notifications for new reviews
- [ ] API rate limiting
- [ ] Real-time updates with SignalR
- [ ] Kubernetes deployment manifests
- [ ] CI/CD pipeline with GitHub Actions

## 📝 License

This project is licensed under the MIT License.

---

## 🆘 Support

If you encounter any issues:
1. Check the troubleshooting section above
2. View logs using `./manage-docker.sh logs`
3. Ensure Docker Desktop is running
4. Try cleaning and restarting: `./manage-docker.sh clean && ./manage-docker.sh start`

**Happy coding! 🎉**
