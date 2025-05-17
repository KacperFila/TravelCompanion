import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { TravelPoint } from '../../models/plan.models';
import { NgIf } from "@angular/common";

@Component({
  selector: 'app-edit-point-modal',
  templateUrl: './edit-point-modal.component.html',
  styleUrls: ['./edit-point-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, NgIf],
})
export class EditPointModalComponent {

  @Input() isModalOpen: boolean = false;
  @Input() pointToEdit!: TravelPoint;
  @Output() closeModalEvent = new EventEmitter<void>();
  @Output() editedPointEvent = new EventEmitter<TravelPoint>();

  closeEditPointModal(): void {
    this.closeModalEvent.emit();
  }

  editedPoint(): void {
    this.editedPointEvent.emit(this.pointToEdit)
  }
}
