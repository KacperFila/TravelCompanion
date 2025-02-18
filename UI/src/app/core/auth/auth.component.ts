import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from './auth.service';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  imports: [FormsModule, CommonModule],
})
export class AuthComponent {
  constructor(private authService: AuthService) {}

  isLoginMode = true;
  error: string | null = null;

  onSubmit(form: NgForm) {
    const email = form.value.email;
    const password = form.value.password;

    if (this.isLoginMode) {
      this.authService.login(email, password).subscribe(
        (response) => {
          console.log(response);
        },
        (error) => {
          console.log(error);
          this.error = error;
        }
      );
    } else {
      this.authService.signup(email, password).subscribe(
        (response) => {
          console.log(response);
        },
        (error) => {
          console.log(error);
          this.error = error;
        }
      );
    }

    console.log(form);
  }

  toggleAuthMode() {
    this.isLoginMode = !this.isLoginMode;
  }
}
