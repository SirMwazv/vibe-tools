import { Tool, CreateToolDto, CreateReviewDto, Review } from '@/types';

const API_BASE = process.env.NEXT_PUBLIC_API_URL || 'https://localhost:5140/api';

export class ApiService {
  private static async fetchApi<T>(endpoint: string, options?: RequestInit): Promise<T> {
    const response = await fetch(`${API_BASE}${endpoint}`, {
      ...options,
      headers: {
        'Content-Type': 'application/json',
        ...options?.headers,
      },
    });

    if (!response.ok) {
      throw new Error(`API Error: ${response.status} ${response.statusText}`);
    }

    return response.json();
  }

  static async getTools(search?: string): Promise<Tool[]> {
    const queryParams = search ? `?search=${encodeURIComponent(search)}` : '';
    return this.fetchApi<Tool[]>(`/tools${queryParams}`);
  }

  static async getTool(id: number): Promise<Tool> {
    return this.fetchApi<Tool>(`/tools/${id}`);
  }

  static async createTool(tool: CreateToolDto): Promise<Tool> {
    return this.fetchApi<Tool>('/tools', {
      method: 'POST',
      body: JSON.stringify(tool),
    });
  }

  static async createReview(toolId: number, review: CreateReviewDto): Promise<Review> {
    return this.fetchApi<Review>(`/tools/${toolId}/reviews`, {
      method: 'POST',
      body: JSON.stringify(review),
    });
  }

  static async getToolReviews(toolId: number): Promise<Review[]> {
    return this.fetchApi<Review[]>(`/tools/${toolId}/reviews`);
  }
}

// Mock API for development/testing when backend is not available
export const mockApi = {
  async getTools(search?: string): Promise<Tool[]> {
    // Simulate API delay
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const mockTools: Tool[] = [
      {
        id: 1,
        name: "Claude",
        description: "Advanced AI assistant by Anthropic for complex reasoning and coding tasks",
        category: "AI Assistant",
        url: "https://claude.ai",
        isCommunityFavorite: true,
        averageRating: 4.8,
        reviewCount: 12,
        createdAt: "2024-01-15T10:00:00Z",
        reviews: [
          { id: 1, toolId: 1, rating: 5, comment: "It's morphin' time! This AI assistant is as powerful as the Red Ranger!", reviewerName: "Jason Lee Scott", createdAt: "2024-01-20T10:00:00Z" },
          { id: 2, toolId: 1, rating: 4, comment: "Blue Ranger approves! Great for analytical tasks and problem-solving.", reviewerName: "Billy Cranston", createdAt: "2024-01-19T10:00:00Z" }
        ]
      },
      {
        id: 2,
        name: "GitHub Copilot",
        description: "AI-powered code completion and suggestion tool",
        category: "IDE Extension",
        url: "https://github.com/features/copilot",
        isCommunityFavorite: false,
        averageRating: 4.5,
        reviewCount: 8,
        createdAt: "2024-01-10T10:00:00Z",
        reviews: [
          { id: 3, toolId: 2, rating: 5, comment: "Space Rangers unite! This coding assistant is out of this world!", reviewerName: "Andros", createdAt: "2024-01-18T10:00:00Z" },
          { id: 4, toolId: 2, rating: 4, comment: "Pink Space Ranger loves the autocomplete features!", reviewerName: "Cassie Chan", createdAt: "2024-01-17T10:00:00Z" }
        ]
      },
      {
        id: 3,
        name: "Cursor",
        description: "AI-first code editor built for pair programming with AI",
        category: "IDE",
        url: "https://cursor.sh",
        isCommunityFavorite: false,
        averageRating: 4.3,
        reviewCount: 5,
        createdAt: "2024-01-12T10:00:00Z",
        reviews: [
          { id: 5, toolId: 3, rating: 5, comment: "Galaxy Red Ranger reporting - this AI-first editor is stellar!", reviewerName: "Leo Corbett", createdAt: "2024-01-16T10:00:00Z" }
        ]
      },
      {
        id: 4,
        name: "Midjourney",
        description: "AI image generation tool for creating stunning artwork",
        category: "Creative",
        url: "https://midjourney.com",
        isCommunityFavorite: false,
        averageRating: 4.7,
        reviewCount: 15,
        createdAt: "2024-01-08T10:00:00Z",
        reviews: [
          { id: 6, toolId: 4, rating: 5, comment: "Galaxy Pink Ranger here - creates images more beautiful than Mirinoi!", reviewerName: "Kendrix Morgan", createdAt: "2024-01-15T10:00:00Z" }
        ]
      }
    ];

    if (search) {
      return mockTools.filter(tool => 
        tool.name.toLowerCase().includes(search.toLowerCase()) ||
        tool.description.toLowerCase().includes(search.toLowerCase()) ||
        tool.category.toLowerCase().includes(search.toLowerCase())
      );
    }
    
    return mockTools.sort((a, b) => b.averageRating - a.averageRating);
  },

  async getTool(id: number): Promise<Tool | null> {
    const tools = await this.getTools();
    return tools.find(t => t.id === id) || null;
  },

  async createTool(tool: CreateToolDto): Promise<Tool> {
    await new Promise(resolve => setTimeout(resolve, 500));
    return {
      id: Date.now(),
      ...tool,
      isCommunityFavorite: false,
      averageRating: 0,
      reviewCount: 0,
      createdAt: new Date().toISOString(),
      reviews: []
    };
  },

  async createReview(toolId: number, review: CreateReviewDto): Promise<Review> {
    await new Promise(resolve => setTimeout(resolve, 500));
    return {
      id: Date.now(),
      toolId,
      ...review,
      createdAt: new Date().toISOString()
    };
  }
};
