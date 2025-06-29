import React, { useState } from 'react';
import { Star } from 'lucide-react';
import styles from './StarRating.module.css';

interface StarRatingProps {
  rating: number;
  size?: 'sm' | 'md' | 'lg';
  interactive?: boolean;
  onChange?: (rating: number) => void;
}

const StarRating: React.FC<StarRatingProps> = ({ 
  rating, 
  size = 'md', 
  interactive = false, 
  onChange 
}) => {
  const [hoverRating, setHoverRating] = useState(0);

  return (
    <div className={styles.container}>
      {[1, 2, 3, 4, 5].map((star) => (
        <Star
          key={star}
          className={`${styles.star} ${styles[size]} ${
            star <= (hoverRating || rating) ? styles.filled : styles.empty
          } ${interactive ? styles.interactive : ''}`}
          onClick={interactive ? () => onChange?.(star) : undefined}
          onMouseEnter={interactive ? () => setHoverRating(star) : undefined}
          onMouseLeave={interactive ? () => setHoverRating(0) : undefined}
        />
      ))}
    </div>
  );
};

export default StarRating;
