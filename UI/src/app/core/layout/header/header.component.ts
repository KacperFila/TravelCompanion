import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { User } from '../../auth/user.model';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule],
})
export class HeaderComponent {
  constructor(
    private authService: AuthService,
    private router: Router)
  {
    this.user$ = this.authService.user;
  }

  user$: Observable<User | null>;
  error: string | null = null;

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }
}
