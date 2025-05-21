import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ModalComponent } from "../../../../shared/modal/modal.component";
import { PlansService } from "../../services/plans/plans.service";

@Component({
  selector: 'app-accept-plan-modal',
  templateUrl: './accept-plan-modal.component.html',
  styleUrls: ['./accept-plan-modal.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, ModalComponent],
})
export class AcceptPlanModalComponent {
  constructor(private plansService: PlansService) {}

  @Input() isModalOpen: boolean = false;
  @Input() planTitle: string = '';
  @Input() planId: string = '';
  @Output() closeModalEvent = new EventEmitter<void>();

  closeModal(): void {
    this.closeModalEvent.emit();
  }

  acceptPlan(): void {
    this.plansService.acceptTravelPlan(this.planId)
      .subscribe(
        () => this.closeModal()
      );
  }
}
