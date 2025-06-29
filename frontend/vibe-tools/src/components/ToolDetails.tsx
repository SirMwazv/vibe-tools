import React, { useState } from 'react';
import { Plus, ExternalLink } from 'lucide-react';
import { Tool, CreateReviewDto } from '@/types';
import StarRating from './StarRating';
import { ApiService } from '@/services/api';
import styles from './ToolDetails.module.css';

interface ToolDetailsProps {
  tool: Tool;
  onBack: () => void;
  onReviewAdded: () => void;
}

const ToolDetails: React.FC<ToolDetailsProps> = ({ 
  tool, 
  onBack, 
  onReviewAdded 
}) => {
  const [showReviewForm, setShowReviewForm] = useState(false);
  const [reviewForm, setReviewForm] = useState<CreateReviewDto>({
    rating: 5,
    comment: '',
    reviewerName: ''
  });
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmitReview = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!reviewForm.reviewerName.trim()) return;
    
    setIsSubmitting(true);
    try {
      await ApiService.createReview(tool.id, reviewForm);
      setShowReviewForm(false);
      setReviewForm({ rating: 5, comment: '', reviewerName: '' });
      onReviewAdded();
    } catch (error) {
      console.error('Failed to submit review:', error);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className={styles.container}>
      <button
        onClick={onBack}
        className={styles.backButton}
      >
        ← Back to Tools
      </button>

      <div className={styles.card}>
        <div className={styles.header}>
          <div>
            <div className={`${styles.titleContainer} ${tool.isCommunityFavorite ? styles.communityFavorite : ''}`}>
              <h1 className={styles.title}>{tool.name}</h1>
              {tool.isCommunityFavorite && (
                <div className={styles.communityBadge}>
                  ⭐ Community Favourite
                </div>
              )}
            </div>
            <span className={styles.category}>
              {tool.category}
            </span>
          </div>
          <a
            href={tool.url}
            target="_blank"
            rel="noopener noreferrer"
            className={styles.visitButton}
          >
            <ExternalLink className={styles.buttonIcon} />
            <span>Visit Tool</span>
          </a>
        </div>

        <p className={styles.description}>{tool.description}</p>

        <div className={styles.ratingSection}>
          <StarRating rating={tool.averageRating} size="lg" />
          <span className={styles.ratingValue}>
            {tool.averageRating.toFixed(1)}
          </span>
          <span className={styles.reviewCount}>({tool.reviewCount} reviews)</span>
        </div>

        <div className={styles.reviewsSection}>
          <div className={styles.reviewsHeader}>
            <h2 className={styles.reviewsTitle}>Reviews</h2>
            <button
              onClick={() => setShowReviewForm(!showReviewForm)}
              className={styles.addReviewButton}
            >
              <Plus style={{ width: '1rem', height: '1rem' }} />
              <span>Add Review</span>
            </button>
          </div>

          {showReviewForm && (
            <form onSubmit={handleSubmitReview} className={styles.reviewForm}>
              <div className={styles.formGroup}>
                <label className={styles.label}>
                  Your Name
                </label>
                <input
                  type="text"
                  value={reviewForm.reviewerName}
                  onChange={(e) => setReviewForm(prev => ({ ...prev, reviewerName: e.target.value }))}
                  className={styles.input}
                  required
                />
              </div>
              
              <div className={styles.formGroup}>
                <label className={styles.label}>
                  Rating
                </label>
                <StarRating
                  rating={reviewForm.rating}
                  size="lg"
                  interactive
                  onChange={(rating) => setReviewForm(prev => ({ ...prev, rating }))}
                />
              </div>
              
              <div className={styles.formGroup}>
                <label className={styles.label}>
                  Comment (optional)
                </label>
                <textarea
                  value={reviewForm.comment}
                  onChange={(e) => setReviewForm(prev => ({ ...prev, comment: e.target.value }))}
                  rows={3}
                  className={styles.textarea}
                />
              </div>
              
              <div className={styles.formActions}>
                <button
                  type="submit"
                  disabled={isSubmitting}
                  className={styles.submitButton}
                >
                  {isSubmitting ? 'Submitting...' : 'Submit Review'}
                </button>
                <button
                  type="button"
                  onClick={() => setShowReviewForm(false)}
                  className={styles.cancelButton}
                >
                  Cancel
                </button>
              </div>
            </form>
          )}

          <div className={styles.reviewsList}>
            {tool.reviews.length === 0 ? (
              <p className={styles.noReviews}>No reviews yet. Be the first to review this tool!</p>
            ) : (
              tool.reviews.map((review) => (
                <div key={review.id} className={styles.reviewItem}>
                  <div className={styles.reviewHeader}>
                    <div className={styles.reviewerInfo}>
                      <span className={styles.reviewerName}>{review.reviewerName}</span>
                      <StarRating rating={review.rating} size="sm" />
                    </div>
                    <span className={styles.reviewDate}>
                      {new Date(review.createdAt).toLocaleDateString()}
                    </span>
                  </div>
                  {review.comment && (
                    <p className={styles.reviewComment}>{review.comment}</p>
                  )}
                </div>
              ))
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default ToolDetails;
