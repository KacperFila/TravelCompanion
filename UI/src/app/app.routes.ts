import { Routes } from '@angular/router';
import { AuthComponent } from './core/auth/auth.component';
import { AuthGuard } from './core/auth/auth.guard';
import { TravelsDashboardComponent } from './core/layout/travels-dashboard/travels-dashboard.component';
import { CostsDashboardComponent } from './core/layout/costs-dashboard/costs-dashboard.component';
import { PlansDashboardComponent } from './core/layout/plans-dashboard/plans-dashboard.component';
import {WildcardRedirectGuard} from "./core/auth/wildcard.guard";
import {RedirectComponent} from "./core/shared/redirect/redirect.component";

export const routes: Routes = [
  {
    path: 'auth',
    component: AuthComponent
  },
  {
    path: '',
    redirectTo: 'travels',
    pathMatch: 'full'
  },
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
  {
    path: '**',
    component: RedirectComponent,
    canActivate: [WildcardRedirectGuard]
  }
];
