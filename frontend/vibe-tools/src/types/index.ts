// Types for the VibeTools application
export interface Tool {
  id: number;
  name: string;
  description: string;
  category: string;
  url: string;
  isCommunityFavorite: boolean;
  averageRating: number;
  reviewCount: number;
  createdAt: string;
  reviews: Review[];
}

export interface Review {
  id: number;
  toolId: number;
  rating: number;
  comment: string;
  reviewerName: string;
  createdAt: string;
}

export interface CreateToolDto {
  name: string;
  description: string;
  category: string;
  url: string;
}

export interface CreateReviewDto {
  rating: number;
  comment: string;
  reviewerName: string;
}

export interface ToolDto {
  id: number;
  name: string;
  description: string;
  category: string;
  url: string;
  isCommunityFavorite: boolean;
  averageRating: number;
  reviewCount: number;
  createdAt: string;
  reviews: ReviewDto[];
}

export interface ReviewDto {
  id: number;
  toolId: number;
  rating: number;
  comment: string;
  reviewerName: string;
  createdAt: string;
}
