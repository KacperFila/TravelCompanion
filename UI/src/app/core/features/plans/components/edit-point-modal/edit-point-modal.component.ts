import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { TravelPoint } from '../../models/plan.models';
import { ItemListComponent } from "../../../../shared/item-list/item-list.components";
import { NgIf } from "@angular/common";

@Component({
  selector: 'app-edit-point-modal',
  templateUrl: './edit-point-modal.component.html',
  styleUrls: ['./edit-point-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, ItemListComponent, NgIf],
})
export class EditPointModalComponent {

  @Input() isModalOpen: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();
  @Output() editedPointEvent = new EventEmitter<TravelPoint>();

  editedTravelPoint: TravelPoint = { id: '', placeName: '', totalCost: 0, travelPlanOrderNumber: 0 };

  closeEditPointModal(): void {
    this.closeModalEvent.emit();
  }
}
