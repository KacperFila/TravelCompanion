import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { User } from '../../auth/user.model';
import { Invitation } from "../../features/plans/models/plan.models";
import { HttpClient } from "@angular/common/http";
import {environment} from "../../../../environments/environment";

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  imports: [CommonModule, RouterModule],
})
export class HeaderComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router, private httpClient: HttpClient)
  {
    this.user$ = this.authService.user;
  }

  ngOnInit(): void {

  }

  user$: Observable<User | null>;
  invitations$: Observable<Invitation[]> = new Observable<Invitation[]>();
  error: string | null = null;
  private invitations: Invitation[] = [];

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }

  fetchInvitations(userId: string) {
    // return this.httpClient
    //   .get(
    //     `${environment.apiBaseUrl}/travelplans-module/Point/Update/${updateRequestId}/Acceptance`,
    //     {}
    //   );
  }
}
