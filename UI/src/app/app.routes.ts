import { Routes } from '@angular/router';
import { AuthComponent } from './core/auth/auth.component';
import { AuthGuard } from './core/auth/auth.guard';
import { HomeComponent } from './core/components/home/home.component';
import { TravelsDashboardComponent } from './core/components/travels-dashboard/travels-dashboard.component';

export const routes: Routes = [
  { path: 'auth', component: AuthComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  {
    path: 'travels',
    component: TravelsDashboardComponent,
    canActivate: [AuthGuard],
  },
  { path: 'plans', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'costs', component: HomeComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: 'auth' },
];
