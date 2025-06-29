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
