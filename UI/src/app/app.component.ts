import { Component } from '@angular/core';
import { AuthComponent } from './core/auth/auth.component';
import { HeaderComponent } from './core/layout/header/header.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [AuthComponent, HeaderComponent, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'UI';
}
