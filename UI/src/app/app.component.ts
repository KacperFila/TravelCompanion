import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from './core/layout/header/header.component';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from './core/auth/auth.service';
import {SignalRService} from "./core/shared/services/signalr.service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeaderComponent, RouterOutlet, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthService, private signalRService: SignalRService) { }
  ngOnInit(): void {
    this.authService.autoLogin();
    this.signalRService.startConnection()
  }
  title = 'UI';
}
