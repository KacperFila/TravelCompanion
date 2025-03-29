import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from './core/layout/header/header.component';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from './core/auth/auth.service';
import { NotificationComponent } from "./core/shared/notification/notification.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeaderComponent, RouterOutlet, RouterModule, NotificationComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthService) { }
  ngOnInit(): void {
    this.authService.autoLogin();
  }
  title = 'UI';
}
