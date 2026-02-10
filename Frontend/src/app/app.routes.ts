import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guards';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./auth/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./dashboard/dashboard.component').then(m => m.DashboardComponent),
    canActivate: [authGuard]
  },
  {
    path: 'products/:categoryId',
    loadComponent: () => import('./products/product-list/product-list.component').then(m => m.ProductListComponent),  // ⭐ Updated path
    canActivate: [authGuard]
  },
  {
    path: 'products/:categoryId/detail/:productId',
    loadComponent: () => import('./products/product-detail/product-detail.component').then(m => m.ProductDetailComponent),
    canActivate: [authGuard]
  },
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];