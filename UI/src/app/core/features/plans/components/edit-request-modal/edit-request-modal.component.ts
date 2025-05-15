import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { TravelPointUpdateRequest } from '../../models/plan.models';
import { ItemListComponent } from "../../../../shared/item-list/item-list.components";
import { NgIf } from "@angular/common";
import { PlansService } from "../../services/plans/plans.service";

@Component({
  selector: 'app-edit-request-modal',
  templateUrl: './edit-request-modal.component.html',
  styleUrls: ['./edit-request-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, ItemListComponent, NgIf],
})
export class EditRequestModalComponent {
  constructor(private plansService: PlansService) {
  }

  @Input() isModalOpen: boolean = false;
  @Input() updateRequests: TravelPointUpdateRequest[] = [];

  @Output() acceptRequestEvent = new EventEmitter<void>();
  @Output() rejectRequestEvent = new EventEmitter<void>();
  @Output() closeModalEvent = new EventEmitter<void>();

  closeEditRequestModal(): void {
    this.closeModalEvent.emit();
  }

  acceptUpdateRequest(updateRequest: TravelPointUpdateRequest)
  {
    this.plansService.acceptUpdateRequest(updateRequest.requestId)
      .subscribe();

    this.closeModalEvent.emit();
  }

  rejectUpdateRequest(updateRequest: TravelPointUpdateRequest)
  {
    this.plansService.rejectUpdateRequest(updateRequest.requestId)
      .subscribe();
    this.closeModalEvent.emit();
  }
}
