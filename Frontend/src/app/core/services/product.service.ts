import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { 
  Product, 
  CreateProduct, 
  UpdateProductPrice 
} from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = `${environment.apiUrl}${API_ENDPOINTS.PRODUCTS.BASE}`;

  constructor(private http: HttpClient) {}

  /**
   * Get all products
   */
  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl);
  }

  /**
   * Get product by ID
   */
  getProductById(id: number): Observable<Product> {
    const url = `${environment.apiUrl}${API_ENDPOINTS.PRODUCTS.BY_ID(id)}`;
    return this.http.get<Product>(url);
  }

  /**
   * Get products by category
   */
  getProductsByCategory(categoryId: number): Observable<Product[]> {
    const url = `${environment.apiUrl}${API_ENDPOINTS.PRODUCTS.BY_CATEGORY(categoryId)}`;
    return this.http.get<Product[]>(url);
  }

  /**
   * Create new product
   */
  createProduct(product: CreateProduct): Observable<Product> {
    return this.http.post<Product>(this.baseUrl, product);
  }

  /**
   * Update product price
   */
  updateProductPrice(id: number, priceUpdate: UpdateProductPrice): Observable<Product> {
    const url = `${environment.apiUrl}${API_ENDPOINTS.PRODUCTS.UPDATE_PRICE(id)}`;
    return this.http.put<Product>(url, priceUpdate);
  }

  /**
   * Delete product
   */
  deleteProduct(id: number): Observable<void> {
    const url = `${environment.apiUrl}${API_ENDPOINTS.PRODUCTS.DELETE(id)}`;
    return this.http.delete<void>(url);
  }
}