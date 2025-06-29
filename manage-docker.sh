#!/bin/bash

# VibeTools Docker Management Script
# This script helps manage the entire VibeTools application with Docker

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Function to print colored output
print_status() {
    echo -e "${BLUE}[INFO]${NC} $1"
}

print_success() {
    echo -e "${GREEN}[SUCCESS]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Function to check if Docker is running and detect compose command
check_docker() {
    if ! docker info >/dev/null 2>&1; then
        print_error "Docker is not running. Please start Docker and try again."
        exit 1
    fi
    
    # Check which compose command to use
    if command -v docker-compose >/dev/null 2>&1; then
        DOCKER_COMPOSE="docker-compose"
    elif docker compose version >/dev/null 2>&1; then
        DOCKER_COMPOSE="docker compose"
    else
        print_error "Docker Compose is not available. Please install Docker Compose."
        exit 1
    fi
}

# Function to build and start all services
start_all() {
    print_status "Starting VibeTools application..."
    check_docker
    
    print_status "Building and starting all services..."
    $DOCKER_COMPOSE up --build -d
    
    print_status "Waiting for services to be ready..."
    sleep 10
    
    print_status "Running database migrations..."
    $DOCKER_COMPOSE exec api dotnet ef database update --no-build || true
    
    print_success "VibeTools application is running!"
    echo ""
    echo "🌐 Frontend: http://localhost:3000"
    echo "🔗 API: http://localhost:5140"
    echo "🗄️  Database: localhost:1433"
    echo ""
    echo "Use '$DOCKER_COMPOSE logs -f' to view logs"
    echo "Use './manage-docker.sh stop' to stop all services"
}

# Function to stop all services
stop_all() {
    print_status "Stopping VibeTools application..."
    check_docker
    $DOCKER_COMPOSE down
    print_success "All services stopped."
}

# Function to restart all services
restart_all() {
    print_status "Restarting VibeTools application..."
    stop_all
    start_all
}

# Function to view logs
view_logs() {
    check_docker
    service=${1:-""}
    if [ -z "$service" ]; then
        print_status "Showing logs for all services..."
        $DOCKER_COMPOSE logs -f
    else
        print_status "Showing logs for $service..."
        $DOCKER_COMPOSE logs -f "$service"
    fi
}

# Function to clean up everything
clean_all() {
    check_docker
    print_warning "This will remove all containers, images, and volumes. Are you sure? (y/N)"
    read -r response
    if [[ "$response" =~ ^([yY][eE][sS]|[yY])$ ]]; then
        print_status "Cleaning up Docker resources..."
        $DOCKER_COMPOSE down -v --rmi all --remove-orphans
        print_success "Cleanup completed."
    else
        print_status "Cleanup cancelled."
    fi
}

# Function to show status
show_status() {
    check_docker
    print_status "Service status:"
    $DOCKER_COMPOSE ps
}

# Function to run database migrations
run_migrations() {
    check_docker
    print_status "Running database migrations..."
    $DOCKER_COMPOSE exec api dotnet ef database update --no-build
    print_success "Migrations completed."
}

# Function to seed database
seed_database() {
    check_docker
    print_status "Seeding database with sample data..."
    # The seeding happens automatically when the API starts
    # But we can trigger it manually if needed
    $DOCKER_COMPOSE restart api
    print_success "Database seeded with sample data."
}

# Function to open shell in container
shell() {
    check_docker
    service=${1:-"api"}
    print_status "Opening shell in $service container..."
    $DOCKER_COMPOSE exec "$service" /bin/sh
}

# Help function
show_help() {
    echo "VibeTools Docker Management Script"
    echo ""
    echo "Usage: $0 [COMMAND]"
    echo ""
    echo "Commands:"
    echo "  start       Start all services (build if needed)"
    echo "  stop        Stop all services"
    echo "  restart     Restart all services"
    echo "  status      Show service status"
    echo "  logs        Show logs for all services"
    echo "  logs [svc]  Show logs for specific service (api, frontend, sqlserver)"
    echo "  migrate     Run database migrations"
    echo "  seed        Seed database with sample data"
    echo "  shell [svc] Open shell in container (default: api)"
    echo "  clean       Remove all containers, images, and volumes"
    echo "  help        Show this help message"
    echo ""
    echo "Examples:"
    echo "  $0 start                 # Start the entire application"
    echo "  $0 logs api             # Show API logs"
    echo "  $0 shell frontend       # Open shell in frontend container"
    echo ""
    echo "Manual Docker Compose commands:"
    echo "  docker compose up -d     # Start services"
    echo "  docker compose logs -f   # View logs"
    echo "  docker compose down      # Stop services"
}

# Main script logic
case "${1:-help}" in
    start)
        start_all
        ;;
    stop)
        stop_all
        ;;
    restart)
        restart_all
        ;;
    status)
        show_status
        ;;
    logs)
        view_logs "$2"
        ;;
    migrate)
        run_migrations
        ;;
    seed)
        seed_database
        ;;
    shell)
        shell "$2"
        ;;
    clean)
        clean_all
        ;;
    help|--help|-h)
        show_help
        ;;
    *)
        print_error "Unknown command: $1"
        echo ""
        show_help
        exit 1
        ;;
esac
