import { Component, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../core/services/product.service';
import { CreateProduct, Product } from '../../core/models/product.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {
  @Input() categoryId!: number;
  @Output() productCreated = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  productData: CreateProduct = {
    name: '',
    description: '',
    price: 0,
    categoryId: 0
  };

  isSubmitting: boolean = false;
  errorMessage: string = '';

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productData.categoryId = this.categoryId;
  }

  onSubmit(): void {
    // Validation
    if (!this.productData.name.trim()) {
      this.errorMessage = 'Product name is required';
      return;
    }

    if (this.productData.price <= 0) {
      this.errorMessage = 'Price must be greater than 0';
      return;
    }

    if (this.productData.price > 1000) {
      this.errorMessage = 'Price must be less than or equal to $1000';
      return;
    }

    // Check decimal places (max 2)
    const decimalPlaces = (this.productData.price.toString().split('.')[1] || '').length;
    if (decimalPlaces > 2) {
      this.errorMessage = 'Price can have at most 2 decimal places';
      return;
    }

    // Round to 2 decimal places
    this.productData.price = Math.round(this.productData.price * 100) / 100;

    this.isSubmitting = true;
    this.errorMessage = '';

    this.productService.createProduct(this.productData).subscribe({
      next: (response: Product) => {
        console.log('Product created successfully:', response);
        this.isSubmitting = false;
        this.productCreated.emit();
        this.resetForm();
      },
      error: (error: any) => {
        console.error('Error creating product:', error);
        this.isSubmitting = false;
        this.errorMessage = error.error?.message || 'Failed to create product. Please try again.';
      }
    });
  }

  onCancel(): void {
    this.cancelled.emit();
    this.resetForm();
  }

  resetForm(): void {
    this.productData = {
      name: '',
      description: '',
      price: 0,
      categoryId: this.categoryId
    };
    this.errorMessage = '';
  }
}