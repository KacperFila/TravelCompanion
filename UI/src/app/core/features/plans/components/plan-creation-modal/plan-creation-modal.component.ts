import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { PlansService } from '../../services/plans/plans.service';
import { CreateTravelPlanRequest } from '../../models/plan.models';
import { NgIf, NgFor } from '@angular/common';
import { APIError } from "../../../../shared/models/shared.models";

@Component({
  selector: 'app-create-plan-modal',
  templateUrl: './plan-creation-modal.component.html',
  styleUrls: ['./plan-creation-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, NgIf, NgFor],
})
export class PlanCreationModal {
  constructor(private plansService: PlansService) {}

  @Input() isModalOpen: boolean = false;
  @Output() createPlanEvent = new EventEmitter<void>();
  @Output() closeModalEvent = new EventEmitter<void>();

  errorMessages: string[] = [];

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
          this.handleError(error);
        }
      });
  }

  private handleError(errorResponse: any) {
    const backendErrors: APIError[] = this.extractBackendErrors(errorResponse);

    if (backendErrors.length > 0) {
      this.errorMessages = backendErrors.map((e) => e.message);
    } else {
      this.errorMessages = ['Something went wrong creating the plan.'];
    }
  }

  private extractBackendErrors(errorResponse: any): APIError[] {
    const errorArray = errorResponse?.error?.errors;
    if (Array.isArray(errorArray)) {
      return errorArray.map((err: any) => ({
        code: err.code,
        message: err.message
      }));
    }
    return [];
  }

  closePlanCreationModal(): void {
    this.errorMessages = [];
    this.closeModalEvent.emit();
  }
}
