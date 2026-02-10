import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../core/services/product.service';
import { Product } from '../../core/models/product.model';
import { ProductEditFormComponent } from '../../shared/product-edit-form/product-edit-form.component';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, ProductEditFormComponent],
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  categoryId!: number;
  productId!: number;
  product: Product | null = null;
  loading: boolean = true;
  showEditModal: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.categoryId = +params['categoryId'];
      this.productId = +params['productId'];
      this.loadProduct();
    });
  }

  loadProduct(): void {
    this.loading = true;
    this.productService.getProductById(this.productId).subscribe({
      next: (product) => {
        this.product = product;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading product:', error);
        this.loading = false;
      }
    });
  }

  onEditPrice(): void {
    this.showEditModal = true;
  }

  closeEditModal(): void {
    this.showEditModal = false;
  }

  onProductUpdated(): void {
    this.closeEditModal();
    this.loadProduct();
  }

  goBack(): void {
    this.router.navigate(['/products', this.categoryId]);
  }
}