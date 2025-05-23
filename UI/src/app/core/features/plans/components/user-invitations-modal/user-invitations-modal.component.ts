import {Component, EventEmitter, Input, Output} from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { ItemListComponent } from '../../../../shared/item-list/item-list.components';
import { CommonModule } from '@angular/common';
import { PlanInvitationResponse } from "../../services/plans/plans-signalR-responses.models";
import { PlansService } from "../../services/plans/plans.service";

@Component({
  selector: 'app-user-invitations-modal',
  templateUrl: './user-invitations-modal.component.html',
  styleUrls: ['./user-invitations-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, ItemListComponent, CommonModule],
})
export class UserInvitationsModalComponent {
  constructor(private plansService: PlansService) {
  }

  @Input() invitations: PlanInvitationResponse[] = [];
  @Input() isModalOpen: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();

  closeModal() {
    this.closeModalEvent.emit();
  }

  acceptInvitation(invitation: PlanInvitationResponse) {
    this.plansService
      .acceptPlanInvitation(invitation.invitationId)
      .subscribe(() => {
    });
    this.closeModal();
  }

  rejectInvitation(invitation: PlanInvitationResponse) {
    this.plansService
      .rejectPlanInvitation(invitation.invitationId)
      .subscribe(() => {
    });
    this.closeModal();
  }
}
