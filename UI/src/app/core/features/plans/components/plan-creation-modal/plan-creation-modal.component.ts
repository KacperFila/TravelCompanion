import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { PlansService } from '../../services/plans/plans.service';
import { CreateTravelPlanRequest } from '../../models/plan.models';
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-create-plan-modal',
  templateUrl: './plan-creation-modal.component.html',
  styleUrls: ['./plan-creation-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, NgIf],
})
export class PlanCreationModal {
  constructor(private plansService: PlansService) {}

  @Input() isModalOpen: boolean = false;
  @Output() createPlanEvent = new EventEmitter<void>();
  @Output() closeModalEvent = new EventEmitter<void>();

  errorMessage: string | null = null;

  formData: CreateTravelPlanRequest = {
    title: '',
    description: '',
    from: null,
    to: null,
  };

  createPlan(event: Event): void {
    event.preventDefault();

    this.plansService
      .createPlan(
        this.formData.title,
        this.formData.description,
        this.formData.from,
        this.formData.to
      )
      .subscribe({
        next: () => {
          this.createPlanEvent.emit();
          this.closePlanCreationModal();
        },
        error: (error) => {
          const firstError = error?.error?.errors?.[0]?.message;
          this.errorMessage = firstError || 'Something went wrong creating the plan.';
        }
      });
  }

  closePlanCreationModal(): void {
    this.errorMessage = null;
    this.closeModalEvent.emit();
  }
}
