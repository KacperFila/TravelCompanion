import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { PlansSignalRService } from '../features/plans/services/plans/plans-signalR.service';
import { APIError } from '../shared/models/shared.models';

@Component({
  standalone: true,
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
  imports: [FormsModule, CommonModule],
})
export class AuthComponent {
  constructor(
    private authService: AuthService,
    private router: Router,
    private plansSignalRService: PlansSignalRService
  ) {}

  isLoginMode = true;
  errorMessages: string[] = [];

  credentials = {
    email: '',
    password: ''
  };

  onSubmit(form: NgForm) {
    if (!form.valid) {
      this.errorMessages = ['Please fill out all required fields.'];
      return;
    }

    this.errorMessages = [];

    const { email, password } = this.credentials;

    if (this.isLoginMode) {
      this.authService.login(email, password).subscribe({
        next: () => {
          this.plansSignalRService.startConnection();
          this.router.navigate(['/travels']);
        },
        error: (error) => {
          this.handleError(error);
        }
      });
    } else {
      this.authService.signup(email, password).subscribe({
        next: () => {
          this.isLoginMode = true;
        },
        error: (error) => {
          this.handleError(error);
        }
      });
    }
  }

  toggleAuthMode() {
    this.isLoginMode = !this.isLoginMode;
    this.errorMessages = [];
    this.credentials = { email: '', password: '' };
  }

  private handleError(errorResponse: any) {
    const backendErrors: APIError[] = this.extractBackendErrors(errorResponse);

    if (backendErrors.length > 0) {
      this.errorMessages = backendErrors.map((e) => e.message);
    } else {
      this.errorMessages = ['Unexpected error occurred.'];
    }
  }

  private extractBackendErrors(errorResponse: any): APIError[] {
    const errorArray = errorResponse?.error?.errors;
    if (Array.isArray(errorArray)) {
      return errorArray.map((err: any) => ({
        code: err.code,
        message: err.message
      }));
    }
    return [];
  }
}
