'use client';

import React, { useState, useEffect, useCallback } from 'react';
import { Search, Plus } from 'lucide-react';
import { Tool, CreateToolDto } from '@/types';
import { ApiService } from '@/services/api';
import ToolCard from '@/components/ToolCard';
import AddToolForm from '@/components/AddToolForm';
import { useRouter } from 'next/navigation';
import styles from './page.module.css';

export default function VibeToolsApp() {
  const [tools, setTools] = useState<Tool[]>([]);
  const [showAddForm, setShowAddForm] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');
  const [loading, setLoading] = useState(true);
  const [sort, setSort] = useState<'stars' | 'az' | 'za'>('stars');
  const [minStars, setMinStars] = useState(0);
  const [showAll, setShowAll] = useState(false);

  const router = useRouter();

  const loadTools = useCallback(async () => {
    setLoading(true);
    try {
      const data = await ApiService.getTools(searchQuery);
      setTools(data);
    } catch (error) {
      console.error('Failed to load tools:', error);
    } finally {
      setLoading(false);
    }
  }, [searchQuery]);

  useEffect(() => {
    loadTools();
  }, [loadTools]);

  const handleToolClick = (toolId: number) => {
    router.push(`/tool/${toolId}`);
  };

  const handleAddTool = async (newTool: CreateToolDto) => {
    try {
      const createdTool = await ApiService.createTool(newTool);
      setTools(prev => [createdTool, ...prev]);
      setShowAddForm(false);
    } catch (error) {
      console.error('Failed to create tool:', error);
    }
  };

  const handleReviewAdded = async () => {
    loadTools(); // Refresh the list to update rankings
  };

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    // Search is handled by useEffect when searchQuery changes
  };

  // Filtering and sorting logic
  const filteredTools = tools
    .filter(tool => tool.averageRating >= minStars)
    .sort((a, b) => {
      if (sort === 'stars') return b.averageRating - a.averageRating;
      if (sort === 'az') return a.name.localeCompare(b.name);
      if (sort === 'za') return b.name.localeCompare(a.name);
      return 0;
    });

  // Group tools by category and get top 3 by average rating
  const getTopToolsByCategory = () => {
    const grouped: { [category: string]: Tool[] } = {};
    tools.forEach(tool => {
      if (!grouped[tool.category]) grouped[tool.category] = [];
      grouped[tool.category].push(tool);
    });
    // Sort each group by average rating and take top 3
    Object.keys(grouped).forEach(cat => {
      grouped[cat] = grouped[cat]
        .sort((a, b) => b.averageRating - a.averageRating)
        .slice(0, 3);
    });
    return grouped;
  };

  if (showAddForm) {
    return (
      <div className={styles.page}>
        <div className={styles.container}>
          <AddToolForm
            onSubmit={handleAddTool}
            onCancel={() => setShowAddForm(false)}
          />
        </div>
      </div>
    );
  }

  return (
    <div className={styles.page}>
      {/* Header */}
      <header className={styles.header}>
        <div className={styles.headerContainer}>
          <div className={styles.headerTop}>
            <div>
              <h1>VibeTools</h1>
              <p>Discover and review the best AI tools</p>
            </div>
            <button
              onClick={() => setShowAddForm(true)}
              className={styles.submitButton}
            >
              <Plus className="w-4 h-4" />
              <span>Submit Tool</span>
            </button>
          </div>

          {/* Search */}
          <form onSubmit={handleSearch} className={styles.searchForm}>
            <Search className={styles.searchIcon} />
            <input
              type="text"
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              placeholder="  Search tools by name, description, or category..."
              className={styles.searchInput}
            />
          </form>

          {/* Filters */}
          <div className={styles.filterBar}>
            <label>
              Min Stars:
              <select value={minStars} onChange={e => setMinStars(Number(e.target.value))}>
                <option value={0}>All</option>
                <option value={1}>1+</option>
                <option value={2}>2+</option>
                <option value={3}>3+</option>
                <option value={4}>4+</option>
                <option value={5}>5</option>
              </select>
            </label>
            <label>
              Sort:
              <select value={sort} onChange={e => setSort(e.target.value as any)}>
                <option value="stars">Stars</option>
                <option value="az">A-Z</option>
                <option value="za">Z-A</option>
              </select>
            </label>
          </div>
        </div>
      </header>

      {/* Main Content */}
      <main className={styles.main}>
        {loading ? (
          <div className={styles.loading}>
            <div className={styles.spinner}></div>
          </div>
        ) : filteredTools.length === 0 ? (
          <div className={styles.emptyState}>
            <div className="message">
              {searchQuery ? 'No tools found matching your search.' : 'No tools available yet.'}
            </div>
            <button
              onClick={() => setShowAddForm(true)}
              className="action"
            >
              Be the first to submit a tool!
            </button>
          </div>
        ) : (
          <>
            <div className={styles.resultsHeader}>
              <h2 className={styles.resultsTitle}>
                {searchQuery ? `Search results for "${searchQuery}"` : showAll ? 'All Tools' : 'Suggested Tools'}
                <span className={styles.resultsCount}>({filteredTools.length} tools)</span>
              </h2>
              {!searchQuery && (
                <button onClick={() => setShowAll(v => !v)} className={styles.viewAllButton}>
                  {showAll ? 'Show Suggested Tools' : 'View All Tools'}
                </button>
              )}
            </div>

            {showAll || searchQuery ? (
              <div className={styles.toolsGrid}>
                {filteredTools.map((tool) => (
                  <ToolCard
                    key={tool.id}
                    tool={tool}
                    onClick={() => handleToolClick(tool.id)}
                  />
                ))}
              </div>
            ) : (
              <div>
                {Object.entries(getTopToolsByCategory()).map(([category, topTools]) => (
                  <div key={category} className={styles.categorySection}>
                    <h3 className={styles.categoryTitle}>{category}</h3>
                    <div className={styles.toolsGrid}>
                      {topTools.map(tool => (
                        <ToolCard
                          key={tool.id}
                          tool={tool}
                          onClick={() => handleToolClick(tool.id)}
                        />
                      ))}
                    </div>
                  </div>
                ))}
              </div>
            )}
          </>
        )}
      </main>

      {/* Footer */}
      <footer className={styles.footer}>
        <div className={styles.footerContainer}>
          <p>VibeTools - Discover the best AI tools for your workflow</p>
          <p className="small">Built with React + .NET Core by Mwazvita Mutowo</p>
        </div>
      </footer>
    </div>
  );
}
