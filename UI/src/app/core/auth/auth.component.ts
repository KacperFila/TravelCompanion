import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from './auth.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
  imports: [FormsModule, CommonModule],
})
export class AuthComponent {
  constructor(private authService: AuthService, private router: Router) {}

  isLoginMode = true;
  error: string | null = null;

  onSubmit(form: NgForm) {
    const email = form.value.email;
    const password = form.value.password;

    if (this.isLoginMode) {
      this.authService.login(email, password).subscribe(
        (response) => {
          console.log(response);
          this.router.navigate(['/home']);
        },
        (error) => {
          console.log(error);
          this.error = error;
        }
      );
    } else {
      this.authService.signup(email, password).subscribe(
        (response) => {
          this.isLoginMode = true;
        },
        (error) => {
          this.error = error;
        }
      );
    }
  }

  toggleAuthMode() {
    this.isLoginMode = !this.isLoginMode;
  }
}
