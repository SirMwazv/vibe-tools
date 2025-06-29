import React, { useState } from 'react';
import { CreateToolDto } from '@/types';
import styles from './AddToolForm.module.css';

interface AddToolFormProps {
  onSubmit: (tool: CreateToolDto) => void;
  onCancel: () => void;
}

const AddToolForm: React.FC<AddToolFormProps> = ({ 
  onSubmit, 
  onCancel 
}) => {
  const [form, setForm] = useState<CreateToolDto>({
    name: '',
    description: '',
    category: '',
    url: ''
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(form);
  };

  return (
    <div className={styles.container}>
      <h2 className={styles.title}>Submit New Tool</h2>
      
      <form onSubmit={handleSubmit} className={styles.form}>
        <div className={styles.field}>
          <label className={styles.label}>
            Tool Name *
          </label>
          <input
            type="text"
            value={form.name}
            onChange={(e) => setForm(prev => ({ ...prev, name: e.target.value }))}
            className={styles.input}
            required
          />
        </div>
        
        <div className={styles.field}>
          <label className={styles.label}>
            Description *
          </label>
          <textarea
            value={form.description}
            onChange={(e) => setForm(prev => ({ ...prev, description: e.target.value }))}
            rows={3}
            className={styles.textarea}
            required
          />
        </div>
        
        <div className={styles.field}>
          <label className={styles.label}>
            Category
          </label>
          <input
            type="text"
            value={form.category}
            onChange={(e) => setForm(prev => ({ ...prev, category: e.target.value }))}
            className={styles.input}
            placeholder="e.g. AI Assistant, IDE, Creative Tool"
          />
        </div>
        
        <div className={styles.fieldLarge}>
          <label className={styles.label}>
            URL
          </label>
          <input
            type="url"
            value={form.url}
            onChange={(e) => setForm(prev => ({ ...prev, url: e.target.value }))}
            className={styles.input}
            placeholder="https://example.com"
          />
        </div>
        
        <div className={styles.actions}>
          <button
            type="submit"
            className={styles.submitButton}
          >
            Submit Tool
          </button>
          <button
            type="button"
            onClick={onCancel}
            className={styles.cancelButton}
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  );
};

export default AddToolForm;
