import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../core/services/product.service';
import { CategoryService } from '../../core/services/category.service';
import { Product } from '../../core/models/product.model';
import { Category } from '../../core/models/category.model';
import { ProductFormComponent } from '../../shared/product-form/product-form.component';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, ProductFormComponent],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  categoryId!: number;
  category: Category | null = null;
  products: Product[] = [];
  loading: boolean = true;
  showCreateProductModal: boolean = false;
  showDeleteConfirmation: boolean = false;
  selectedProduct: Product | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.categoryId = +params['categoryId'];
      this.loadCategoryAndProducts();
    });
  }

  loadCategoryAndProducts(): void {
    this.loading = true;

    this.categoryService.getCategoryById(this.categoryId).subscribe({
      next: (category) => {
        this.category = category;
      },
      error: (error) => {
        console.error('Error loading category:', error);
      }
    });

    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProductsByCategory(this.categoryId).subscribe({
      next: (products) => {
        this.products = products;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading products:', error);
        this.loading = false;
      }
    });
  }

  onProductClick(productId: number, event: Event): void {
    // Don't navigate if clicking on delete button
    const target = event.target as HTMLElement;
    if (target.closest('.delete-hover-button')) {
      return;
    }
    this.router.navigate(['/products', this.categoryId, 'detail', productId]);
  }

  onCreateProduct(): void {
    this.showCreateProductModal = true;
  }

  closeCreateProductModal(): void {
    this.showCreateProductModal = false;
  }

  onProductCreated(): void {
    this.closeCreateProductModal();
    this.loadProducts();
  }

  onDeleteProduct(product: Product, event: Event): void {
    event.stopPropagation();
    this.selectedProduct = product;
    this.showDeleteConfirmation = true;
  }

  closeDeleteConfirmation(): void {
    this.showDeleteConfirmation = false;
    this.selectedProduct = null;
  }

  confirmDelete(): void {
    if (!this.selectedProduct) return;

    this.productService.deleteProduct(this.selectedProduct.id).subscribe({
      next: () => {
        console.log('Product deleted successfully');
        this.closeDeleteConfirmation();
        this.loadProducts();
      },
      error: (error) => {
        console.error('Error deleting product:', error);
        alert('Failed to delete product. Please try again.');
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }
}