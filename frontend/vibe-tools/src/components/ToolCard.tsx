import React from 'react';
import { ExternalLink } from 'lucide-react';
import { Tool } from '@/types';
import StarRating from './StarRating';
import styles from './ToolCard.module.css';

interface ToolCardProps {
  tool: Tool;
  onClick: () => void;
}

const ToolCard: React.FC<ToolCardProps> = ({ tool, onClick }) => (
  <div 
    className={`${styles.card} ${tool.isCommunityFavorite ? styles.communityFavorite : ''}`}
    onClick={onClick}
  >
    {tool.isCommunityFavorite && (
      <div className={styles.communityBadge}>
        ⭐ Community Favourite
      </div>
    )}
    
    <div className={styles.header}>
      <div className={styles.titleSection}>
        <h3 className={styles.title}>{tool.name}</h3>
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
      <ExternalLink className={styles.icon} />
    </div>
  </div>
);

export default ToolCard;
