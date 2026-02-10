import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  
  // Get the token
  const token = authService.getToken();
  
  // Clone the request and add authorization header if token exists
  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }
  
  // Handle the request and catch 401 errors (unauthorized)
  return next(req).pipe(
    catchError((error) => {
      if (error.status === 401) {
        // Token expired or invalid - logout and redirect to login
        authService.logout();
        router.navigate(['/login']);
      }
      return throwError(() => error);
    })
  );
};