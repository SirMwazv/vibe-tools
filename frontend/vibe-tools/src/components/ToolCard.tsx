import React from 'react';
import { ExternalLink, Heart } from 'lucide-react';
import { Tool } from '@/types';
import StarRating from './StarRating';
import styles from './ToolCard.module.css';

interface ToolCardProps {
  tool: Tool;
  onClick: () => void;
}

const ToolCard: React.FC<ToolCardProps> = ({ tool, onClick }) => (
  <div className={styles.card} onClick={onClick}>
    <div className={styles.header}>
      <div className={styles.titleContainer}>
        <h3 className={styles.title}>{tool.name}</h3>
        {tool.isCommunityFavorite && (
          <div title="Community Favorite">
            <Heart className={styles.favoriteIcon} />
          </div>
        )}
      </div>
      <span className={styles.category}>
        {tool.category}
      </span>
    </div>
    
    <p className={styles.description}>{tool.description}</p>
    
    <div className={styles.footer}>
      <div className={styles.rating}>
        <StarRating rating={tool.averageRating} size="sm" />
        <span className={styles.ratingText}>
          {tool.averageRating.toFixed(1)} ({tool.reviewCount} reviews)
        </span>
      </div>
      <ExternalLink className={styles.externalIcon} />
    </div>
  </div>
);

export default ToolCard;
