import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.service';
import { LoginResponse } from '../../core/models/auth.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  //Object holding user input (username & password)
  //Bound to form inputs via [(ngModel)]
  //Two-way binding = When user types, property updates automatically

  loginData = {
    username: '',
    password: ''
  };

  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  //uns when user clicks "Sign In" button
  onSubmit(): void {
    this.errorMessage = '';

    if (!this.loginData.username || !this.loginData.password) {
      //if username and password are empty reset error message and return
      this.errorMessage = 'Please enter both username and password';
      return;
    }

    this.isLoading = true;

    //Call AuthService's login method
    this.authService.login(this.loginData).subscribe({
      
      next: (response: LoginResponse) => {
        //On successful login, response contains the JWT token and user info
        console.log('Login successful!', response);
        this.isLoading = false;
        
        // alert('Login successful! Token: ' + response.token.substring(0, 20) + '...');
        this.router.navigate(['/dashboard']); //redirect to dashboard on successful login
      },
      error: (error: any) => {
        console.error('Login error:', error);
        this.isLoading = false;
        
        if (error.status === 401) {
          this.errorMessage = 'Invalid username or password';
        } else if (error.status === 0) {
          this.errorMessage = 'Cannot connect to server. Please check if backend is running.';
        } else {
          this.errorMessage = error.error?.message || 'Login failed. Please try again.';
        }
      }
    });
  }
}