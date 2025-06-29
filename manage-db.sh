#!/bin/bash

# VibeTools Database Management Script

# Colors for output
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

# Set up PATH for EF tools
export PATH="$PATH:/Users/mwazvitamutowo/.dotnet/tools"

# Change to project directory
cd "$(dirname "$0")/VibeTools"

echo -e "${GREEN}🛠️  VibeTools Database Management${NC}"
echo "=================================="

case "$1" in
  "start-db")
    echo -e "${YELLOW}Starting SQL Server container...${NC}"
    cd ..
    docker compose up -d
    echo -e "${GREEN}✅ SQL Server container started${NC}"
    ;;
  
  "stop-db")
    echo -e "${YELLOW}Stopping SQL Server container...${NC}"
    cd ..
    docker compose down
    echo -e "${GREEN}✅ SQL Server container stopped${NC}"
    ;;
  
  "restart-db")
    echo -e "${YELLOW}Restarting SQL Server container...${NC}"
    cd ..
    docker compose restart sqlserver
    echo -e "${GREEN}✅ SQL Server container restarted${NC}"
    ;;
  
  "migrate")
    echo -e "${YELLOW}Applying database migrations...${NC}"
    dotnet ef database update
    echo -e "${GREEN}✅ Migrations applied${NC}"
    ;;
  
  "add-migration")
    if [ -z "$2" ]; then
      echo -e "${RED}❌ Please provide a migration name${NC}"
      echo "Usage: $0 add-migration MigrationName"
      exit 1
    fi
    echo -e "${YELLOW}Creating migration: $2${NC}"
    dotnet ef migrations add "$2"
    echo -e "${GREEN}✅ Migration '$2' created${NC}"
    ;;
  
  "remove-migration")
    echo -e "${YELLOW}Removing last migration...${NC}"
    dotnet ef migrations remove
    echo -e "${GREEN}✅ Last migration removed${NC}"
    ;;
  
  "reset-db")
    echo -e "${YELLOW}Resetting database (this will delete all data)...${NC}"
    read -p "Are you sure? (y/N): " -n 1 -r
    echo
    if [[ $REPLY =~ ^[Yy]$ ]]; then
      dotnet ef database drop --force
      dotnet ef database update
      echo -e "${GREEN}✅ Database reset completed${NC}"
    else
      echo -e "${YELLOW}Database reset cancelled${NC}"
    fi
    ;;
  
  "run")
    echo -e "${YELLOW}Starting application...${NC}"
    dotnet run
    ;;
  
  "logs")
    echo -e "${YELLOW}Showing SQL Server container logs...${NC}"
    cd ..
    docker compose logs sqlserver
    ;;
  
  "status")
    echo -e "${YELLOW}Checking services status...${NC}"
    cd ..
    echo "Docker containers:"
    docker compose ps
    echo
    echo "Database connection test:"
    cd VibeTools
    dotnet ef database update --dry-run
    ;;
  
  *)
    echo "Usage: $0 {command}"
    echo
    echo "Database Commands:"
    echo "  start-db          Start SQL Server container"
    echo "  stop-db           Stop SQL Server container"
    echo "  restart-db        Restart SQL Server container"
    echo "  migrate           Apply pending migrations"
    echo "  add-migration     Create new migration (requires name)"
    echo "  remove-migration  Remove last migration"
    echo "  reset-db          Drop and recreate database (destructive)"
    echo "  logs              Show SQL Server container logs"
    echo "  status            Check services status"
    echo
    echo "Application Commands:"
    echo "  run               Start the VibeTools API"
    echo
    echo "Examples:"
    echo "  $0 start-db"
    echo "  $0 add-migration AddNewFeature"
    echo "  $0 run"
    exit 1
    ;;
esac
