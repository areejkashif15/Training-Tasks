import { Component, EventEmitter, Output, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CategoryService } from '../../core/services/category.service';
import { CreateCategory } from '../../core/models/category.model';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent {
  @Output() categoryCreated = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  categoryData: CreateCategory = {
    name: '',
    description: ''
  };

  isSubmitting: boolean = false;
  errorMessage: string = '';

  constructor(private categoryService: CategoryService) {}

  onSubmit(): void {
    // Validation
    if (!this.categoryData.name.trim()) {
      this.errorMessage = 'Category name is required';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    this.categoryService.createCategory(this.categoryData).subscribe({
      next: (response) => {
        console.log('Category created successfully:', response);
        this.isSubmitting = false;
        this.categoryCreated.emit();
        this.resetForm();
      },
      error: (error) => {
        console.error('Error creating category:', error);
        this.isSubmitting = false;
        this.errorMessage = error.error?.message || 'Failed to create category. Please try again.';
      }
    });
  }

  onCancel(): void {
    this.cancelled.emit();
    this.resetForm();
  }

  resetForm(): void {
    this.categoryData = {
      name: '',
      description: ''
    };
    this.errorMessage = '';
  }
}