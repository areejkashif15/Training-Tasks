import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment.development';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { LoginRequest, LoginResponse, AuthUser } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<AuthUser | null>;
  public currentUser$: Observable<AuthUser | null>;

  constructor(private http: HttpClient) {
    const storedUser = this.getUserFromStorage();
    this.currentUserSubject = new BehaviorSubject<AuthUser | null>(storedUser);
    this.currentUser$ = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): AuthUser | null {
    return this.currentUserSubject.value;
  }

  public get isLoggedIn(): boolean {
    return !!this.currentUserValue?.token;
  }

  login(credentials: LoginRequest): Observable<LoginResponse> {
    const url = `${environment.apiUrl}${API_ENDPOINTS.AUTH.LOGIN}`;
    
    return this.http.post<LoginResponse>(url, credentials).pipe(
      tap(response => {
        const user: AuthUser = {
          username: credentials.username,
          token: response.token
        };
        this.setUserInStorage(user);
        this.currentUserSubject.next(user);
      })
    );
  }

  logout(): void {
    localStorage.removeItem(environment.tokenKey);
    this.currentUserSubject.next(null);
  }

  getToken(): string | null {
    return this.currentUserValue?.token || null;
  }

  private setUserInStorage(user: AuthUser): void {
    localStorage.setItem(environment.tokenKey, JSON.stringify(user));
  }

  private getUserFromStorage(): AuthUser | null {
    const userJson = localStorage.getItem(environment.tokenKey);
    if (userJson) {
      try {
        return JSON.parse(userJson);
      } catch {
        return null;
      }
    }
    return null;
  }
}