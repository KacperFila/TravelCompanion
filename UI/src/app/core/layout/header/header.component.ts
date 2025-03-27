import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { User } from '../../auth/user.model';
import { PlansSignalRService } from "../../features/plans/services/plans-signalR.service";
import {
  PlanInvitationRemovedResponse,
  PlanInvitationResponse
} from "../../features/plans/services/plans-signalR-responses.models";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ModalComponent} from "../../shared/modal/modal.component";
import {ItemListComponent} from "../../shared/item-list/item-list.components";
import {PlansService} from "../../features/plans/services/plans.service";

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  imports: [CommonModule, RouterModule, FormsModule, ModalComponent, ReactiveFormsModule, ItemListComponent],
})
export class HeaderComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private router: Router,
    private plansSignalRService: PlansSignalRService,
    private plansService: PlansService)
  {
    this.user$ = this.authService.user;
  }

  ngOnInit(): void {
    this.setupSignalRListeners()
  }

  isInvitationsModalOpen: boolean = false;
  user$: Observable<User | null>;
  invitations: PlanInvitationResponse[] =[];
  error: string | null = null;

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }

  private setupSignalRListeners(): void {
    this.plansSignalRService.listenForUpdates("ReceivePlanInvitation",
      (invitation: PlanInvitationResponse) => {
      console.log("Received invitation: ", invitation);
        this.invitations.push(invitation);
    });

    this.plansSignalRService.listenForUpdates("ReceivePlanInvitationRemoved",
      (invitation: PlanInvitationRemovedResponse) => {
        console.log('📌 Received invitation: ', invitation);
      });
  }
}
