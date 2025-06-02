import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TravelPoint } from '../../models/plan.models';
import { CommonModule } from '@angular/common';
import {ModalComponent} from "../../../../shared/modal/modal.component";

@Component({
  selector: 'app-create-plan-point-modal',
  templateUrl: './create-plan-point-modal.component.html',
  styleUrls: ['./create-plan-point-modal.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, ModalComponent],
})
export class CreatePlanPointModal {

  newTravelPoint: TravelPoint = {placeName: '', id: '', totalCost: 0, travelPlanOrderNumber: 0};

  @Input() isModalOpen: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();
  @Output() createdPointEvent = new EventEmitter<TravelPoint>();

  closeModal(): void {
    this.closeModalEvent.emit();
  }

  createPoint(event: Event): void {
    event.preventDefault();
    this.createdPointEvent.emit(this.newTravelPoint);
  }
}
