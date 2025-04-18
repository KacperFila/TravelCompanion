import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from './core/auth/auth.service';
import { NotificationComponent } from './core/shared/notification/notification.component';
import { SidePanelComponent } from './core/layout/side-panel/side-panel.component';
import { Observable } from 'rxjs';
import { User } from './core/auth/user.model';
import {AsyncPipe, NgIf, NgOptimizedImage} from '@angular/common';
import {HeaderComponent} from "./core/layout/header/header.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NotificationComponent, RouterOutlet, RouterModule, SidePanelComponent, AsyncPipe, NgIf, HeaderComponent, NgOptimizedImage],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'UI';
  isSidebarExpanded = false;
  user$: Observable<User | null>;

  constructor(private authService: AuthService) {
    this.user$ = this.authService.user;
  }

  ngOnInit(): void {
    this.authService.autoLogin();
  }

  toggleSidebar(isExpanded: boolean) {
    this.isSidebarExpanded = isExpanded;
  }
}
