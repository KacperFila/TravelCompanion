import { Routes } from '@angular/router';
import { AuthComponent } from './core/auth/auth.component';
import { AuthGuard } from './core/auth/auth.guard';
import { HomeDashboardComponent } from './core/layout/home-dashboard/home-dashboard.component';
import { TravelsDashboardComponent } from './core/layout/travels-dashboard/travels-dashboard.component';
import { CostsDashboardComponent } from './core/layout/costs-dashboard/costs-dashboard.component';
import { PlansDashboardComponent } from './core/layout/plans-dashboard/plans-dashboard.component';

export const routes: Routes = [
  { path: 'auth', component: AuthComponent },
  { path: 'home', component: HomeDashboardComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  {
    path: 'travels',
    component: TravelsDashboardComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'plans',
    component: PlansDashboardComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'costs',
    component: CostsDashboardComponent,
    canActivate: [AuthGuard],
  },
  { path: '**', redirectTo: 'auth' },
];
