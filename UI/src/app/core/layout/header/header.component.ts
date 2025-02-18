import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { environment } from '../../../../environments/environment';

interface TravelResponse {
  title: string;
  description: string;
}

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  imports: [RouterLink, RouterLinkActive],
})
export class HeaderComponent {
  constructor(private http: HttpClient) {}
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
}
