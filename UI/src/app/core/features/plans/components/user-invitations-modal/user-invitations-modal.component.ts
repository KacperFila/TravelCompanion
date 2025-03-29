import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { ItemListComponent } from '../../../../shared/item-list/item-list.components';
import { CommonModule } from '@angular/common';
import {PlanInvitationResponse} from "../../services/plans-signalR-responses.models";
import {PlansService} from "../../services/plans.service";

@Component({
  selector: 'app-user-invitations-modal',
  templateUrl: './user-invitations-modal.component.html',
  styleUrls: ['./user-invitations-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, ItemListComponent, CommonModule],
})
export class UserInvitationsModalComponent implements OnInit {
  constructor(private plansService: PlansService) {
  }

  invitations: PlanInvitationResponse[] = [];
  @Input() isModalOpen: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();

  ngOnInit(): void {
    this.fetchInvitations();
  }

  closeModal() {
    this.isModalOpen = false;
  }

  fetchInvitations() {
    this.plansService.getInvitationsForUser().subscribe((invitations) => {
      this.invitations = invitations;
    });
  }

  acceptInvitation(invitation: PlanInvitationResponse) {
    this.plansService.acceptPlanInvitation(invitation.invitationId).subscribe(() => {
      this.fetchInvitations();
    });
  }

  rejectInvitation(invitation: PlanInvitationResponse) {
    this.plansService.rejectPlanInvitation(invitation.invitationId).subscribe(() => {
      this.fetchInvitations();
    });
  }
}
