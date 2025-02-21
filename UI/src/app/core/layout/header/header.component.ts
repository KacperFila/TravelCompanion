import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { AuthService } from '../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { User } from '../../auth/user.model';

interface TravelResponse {
  title: string;
  description: string;
}

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  imports: [CommonModule, RouterModule],
})
export class HeaderComponent {
  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private router: Router
  ) {
    this.user$ = this.authService.user;
  }

  user$: Observable<User | null>;

  error: string | null = null;

  fetchData() {
    this.http
      .get<TravelResponse[]>(`${environment.apiBaseUrl}/travels-module/Travel`)
      .subscribe(
        (response) => {
          console.log(response);
        },
        (error) => {
          console.log(error);
          this.error = error;
        }
      );
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }
}
