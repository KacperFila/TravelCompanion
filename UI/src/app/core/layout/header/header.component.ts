import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { AuthService } from '../../auth/auth.service';

interface TravelResponse {
  title: string;
  description: string;
}

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  imports: [RouterLink, RouterLinkActive],
})
export class HeaderComponent {
  constructor(private http: HttpClient, private authService: AuthService) {}
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
  }
}
