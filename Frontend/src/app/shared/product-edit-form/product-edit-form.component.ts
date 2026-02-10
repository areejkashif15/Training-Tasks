import { Component, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../core/services/product.service';
import { Product, UpdateProductPrice } from '../../core/models/product.model';

@Component({
  selector: 'app-product-edit-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-edit-form.component.html',
  styleUrls: ['./product-edit-form.component.css']
})
export class ProductEditFormComponent implements OnInit {
  @Input() product!: Product;
  @Output() productUpdated = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  newPrice: number = 0;
  isSubmitting: boolean = false;
  errorMessage: string = '';

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.newPrice = this.product.price;
  }

  onSubmit(): void {
    // Validation
    if (this.newPrice <= 0) {
      this.errorMessage = 'Price must be greater than 0';
      return;
    }

    if (this.newPrice > 1000) {
      this.errorMessage = 'Price must be less than or equal to $1000';
      return;
    }

    // Check decimal places (max 2)
    const decimalPlaces = (this.newPrice.toString().split('.')[1] || '').length;
    if (decimalPlaces > 2) {
      this.errorMessage = 'Price can have at most 2 decimal places';
      return;
    }

    // Round to 2 decimal places
    this.newPrice = Math.round(this.newPrice * 100) / 100;

    this.isSubmitting = true;
    this.errorMessage = '';

    const priceUpdate: UpdateProductPrice = {
      price: this.newPrice
    };

    this.productService.updateProductPrice(this.product.id, priceUpdate).subscribe({
      next: (response: Product) => {
        console.log('Product price updated successfully:', response);
        this.isSubmitting = false;
        this.productUpdated.emit();
      },
      error: (error: any) => {
        console.error('Error updating product price:', error);
        this.isSubmitting = false;
        
        // Handle specific backend validation errors
        if (error.status === 400) {
          this.errorMessage = error.error?.message || 'Invalid price value';
        } else {
          this.errorMessage = 'Failed to update price. Please try again.';
        }
      }
    });
  }

  onCancel(): void {
    this.cancelled.emit();
  }
}