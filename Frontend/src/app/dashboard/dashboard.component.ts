import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../core/services/auth.service';
import { CategoryService } from '../core/services/category.service';
import { ProductService } from '../core/services/product.service';
import { Category } from '../core/models/category.model';
import { CategoryFormComponent } from '../shared/category-form/category-form.component';  // ⭐ Fixed path

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, CategoryFormComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  username: string = '';
  categories: Category[] = [];
  categoryProductCounts: Map<number, number> = new Map();
  loading: boolean = true;
  showCreateCategoryModal: boolean = false;

  constructor(
    private authService: AuthService,
    private categoryService: CategoryService,
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const user = this.authService.currentUserValue;
    this.username = user?.username || 'User';
    this.loadCategories();
  }

  loadCategories(): void {
    this.loading = true;
    
    this.categoryService.getAllCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
        this.loadProductCounts();
      },
      error: (error) => {
        console.error('Error loading categories:', error);
        this.loading = false;
      }
    });
  }

  loadProductCounts(): void {
    this.categories.forEach(category => {
      this.productService.getProductsByCategory(category.id).subscribe({
        next: (products) => {
          this.categoryProductCounts.set(category.id, products.length);
        },
        error: (error) => {
          console.error(`Error loading products for category ${category.id}:`, error);
          this.categoryProductCounts.set(category.id, 0);
        }
      });
    });
    
    this.loading = false;
  }

  getProductCount(categoryId: number): number {
    return this.categoryProductCounts.get(categoryId) || 0;
  }

  onCategoryClick(categoryId: number): void {
    this.router.navigate(['/products', categoryId]);
  }

  onCreateCategory(): void {
    this.showCreateCategoryModal = true;
  }

  closeCreateCategoryModal(): void {
    this.showCreateCategoryModal = false;
  }

  onCategoryCreated(): void {
    this.closeCreateCategoryModal();
    this.loadCategories();
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}