import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from './core/auth/auth.service';
import { NotificationComponent } from './core/shared/notification/notification.component';
import { Observable } from 'rxjs';
import { User } from './core/auth/user.model';
import { AsyncPipe, NgIf } from '@angular/common';
import { HeaderComponent } from "./core/layout/header/header.component";
import {environment} from "../environments/environment.prod";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NotificationComponent, RouterOutlet, RouterModule, AsyncPipe, NgIf, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'UI';
  user$: Observable<User | null>;

  constructor(private authService: AuthService) {
    console.log("Using apiUrl: ", environment.apiBaseUrl);
    this.user$ = this.authService.user;
  }

  ngOnInit(): void {
    this.authService.autoLogin();
  }
}
