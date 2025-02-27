import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalComponent } from '../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';

interface CreateTravelPlanRequest {
  title: string;
  description: string | null;
  from: Date | null;
  to: Date | null;
}

@Component({
  selector: 'app-change-active-plan-modal',
  templateUrl: './change-active-plan-modal.component.html',
  styleUrls: ['./change-active-plan-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule],
})
export class ChangeActivePlanModal {
  @Input() isModalOpen: boolean = false;

  @Output() createPlanEvent = new EventEmitter<CreateTravelPlanRequest>();
  @Output() closeModalEvent = new EventEmitter<void>();

  formData: CreateTravelPlanRequest = {
    title: '',
    description: '',
    from: null,
    to: null,
  };

  createPlan(event: Event): void {
    event.preventDefault();
    this.createPlanEvent.emit(this.formData);
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
