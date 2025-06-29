#!/bin/bash

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Function to print colored output
print_info() {
    echo -e "${BLUE}ℹ️  $1${NC}"
}

print_success() {
    echo -e "${GREEN}✅ $1${NC}"
}

print_warning() {
    echo -e "${YELLOW}⚠️  $1${NC}"
}

print_error() {
    echo -e "${RED}❌ $1${NC}"
}

# Function to check if Docker is running
check_docker() {
    if ! docker info >/dev/null 2>&1; then
        print_error "Docker is not running. Please start Docker and try again."
        exit 1
    fi
}

# Function to show help
show_help() {
    echo "🐳 Vibe Tools Docker Manager"
    echo ""
    echo "Usage: $0 [COMMAND]"
    echo ""
    echo "Commands:"
    echo "  start     Start all services (database, backend, frontend)"
    echo "  stop      Stop all services"
    echo "  restart   Restart all services"
    echo "  build     Build all Docker images"
    echo "  logs      Show logs from all services"
    echo "  logs-db   Show database logs only"
    echo "  logs-api  Show API logs only"
    echo "  logs-web  Show frontend logs only"
    echo "  status    Show status of all containers"
    echo "  clean     Remove all containers and images"
    echo "  reset     Reset everything (clean + rebuild)"
    echo "  shell-db  Connect to database container"
    echo "  shell-api Connect to API container"
    echo "  shell-web Connect to frontend container"
    echo "  help      Show this help message"
    echo ""
    echo "Examples:"
    echo "  $0 start          # Start the application"
    echo "  $0 logs           # View all logs"
    echo "  $0 logs-api       # View only API logs"
    echo "  $0 restart        # Restart everything"
    echo ""
}

# Main script
case "${1:-help}" in
    start)
        check_docker
        print_info "Starting Vibe Tools application..."
        docker compose up -d
        print_success "Application started! 🚀"
        echo ""
        print_info "Services available at:"
        echo "  📱 Frontend: http://localhost:3000"
        echo "  🔧 API: http://localhost:5000"
        echo "  🗄️  Database: localhost:1433"
        echo ""
        print_info "Use '$0 logs' to view application logs"
        ;;
    
    stop)
        check_docker
        print_info "Stopping all services..."
        docker compose down
        print_success "All services stopped."
        ;;
    
    restart)
        check_docker
        print_info "Restarting all services..."
        docker compose down
        docker compose up -d --build
        print_success "All services restarted! 🔄"
        echo ""
        print_info "Services available at:"
        echo "  📱 Frontend: http://localhost:3000"
        echo "  🔧 API: http://localhost:5000"
        echo "  🗄️  Database: localhost:1433"
        ;;
    
    build)
        check_docker
        print_info "Building all Docker images..."
        docker compose build --no-cache
        print_success "All images built successfully! 🔨"
        ;;
    
    logs)
        check_docker
        print_info "Showing logs from all services (Ctrl+C to exit)..."
        docker compose logs -f
        ;;
    
    logs-db)
        check_docker
        print_info "Showing database logs (Ctrl+C to exit)..."
        docker compose logs -f sqlserver
        ;;
    
    logs-api)
        check_docker
        print_info "Showing API logs (Ctrl+C to exit)..."
        docker compose logs -f backend
        ;;
    
    logs-web)
        check_docker
        print_info "Showing frontend logs (Ctrl+C to exit)..."
        docker compose logs -f frontend
        ;;
    
    status)
        check_docker
        print_info "Container status:"
        docker compose ps
        ;;
    
    clean)
        check_docker
        print_warning "This will remove all containers and images. Are you sure? (y/N)"
        read -r response
        if [[ "$response" =~ ^[Yy]$ ]]; then
            print_info "Stopping and removing containers..."
            docker compose down -v
            print_info "Removing images..."
            docker compose down --rmi all
            print_success "Cleanup completed! 🧹"
        else
            print_info "Cleanup cancelled."
        fi
        ;;
    
    reset)
        check_docker
        print_warning "This will reset everything (containers, images, and rebuild). Are you sure? (y/N)"
        read -r response
        if [[ "$response" =~ ^[Yy]$ ]]; then
            print_info "Resetting everything..."
            docker compose down -v --rmi all
            docker compose up -d --build
            print_success "Reset completed! Everything rebuilt from scratch! 🔄"
        else
            print_info "Reset cancelled."
        fi
        ;;
    
    shell-db)
        check_docker
        print_info "Connecting to database container..."
        docker compose exec sqlserver /bin/bash
        ;;
    
    shell-api)
        check_docker
        print_info "Connecting to API container..."
        docker compose exec backend /bin/bash
        ;;
    
    shell-web)
        check_docker
        print_info "Connecting to frontend container..."
        docker compose exec frontend /bin/bash
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
