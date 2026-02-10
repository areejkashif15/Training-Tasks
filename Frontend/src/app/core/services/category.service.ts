import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { Category, CreateCategory } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private baseUrl = `${environment.apiUrl}${API_ENDPOINTS.CATEGORIES.BASE}`;

  constructor(private http: HttpClient) {}

  /**
   * Get all categories
   */
  getAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl);
  }

  /**
   * Get category by ID
   */
  getCategoryById(id: number): Observable<Category> {
    const url = `${environment.apiUrl}${API_ENDPOINTS.CATEGORIES.BY_ID(id)}`;
    return this.http.get<Category>(url);
  }

  /**
   * Create new category
   */
  createCategory(category: CreateCategory): Observable<Category> {
    return this.http.post<Category>(this.baseUrl, category);
  }

  /**
   * Update category
   */
  updateCategory(id: number, category: CreateCategory): Observable<Category> {
    const url = `${environment.apiUrl}${API_ENDPOINTS.CATEGORIES.BY_ID(id)}`;
    return this.http.put<Category>(url, category);
  }

  /**
   * Delete category
   */
  deleteCategory(id: number): Observable<void> {
    const url = `${environment.apiUrl}${API_ENDPOINTS.CATEGORIES.BY_ID(id)}`;
    return this.http.delete<void>(url);
  }
}