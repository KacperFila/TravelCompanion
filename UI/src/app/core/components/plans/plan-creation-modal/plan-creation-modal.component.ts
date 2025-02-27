import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalComponent } from '../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import {
  PlanDetailsDTO,
  PlansService,
} from '../../../layout/plans-dashboard/plans.service';
import { AuthService } from '../../../auth/auth.service';

interface CreateTravelPlanRequest {
  title: string;
  description: string | null;
  from: Date | null;
  to: Date | null;
}

@Component({
  selector: 'app-create-plan-modal',
  templateUrl: './plan-creation-modal.component.html',
  styleUrls: ['./plan-creation-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule],
})
export class PlanCreationModal {
  constructor(
    private plansService: PlansService,
    private authService: AuthService
  ) {}

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
    this.plansService
      .createPlan(
        this.formData.title,
        this.formData.description,
        this.formData.from,
        this.formData.to
      )
      .subscribe(
        (res) => {
          if (!this.authService.user.value?.id) {
            return;
          }
          this.plansService
            .getActivePlan(this.authService.user.value.id)
            .subscribe(
              (activePlan: PlanDetailsDTO) => {
                console.log(activePlan);
                this.setUserActivePlanId(activePlan.id); // Setting the active plan id based on response
                this.closeModalEvent.emit();
              },
              (error) => {
                console.log('Error fetching active plan:', error);
              }
            );
        },
        (error) => {
          console.log('Error creating plan:', error);
        }
      );
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }

  private setUserActivePlanId(planId: string) {
    const user = this.authService.user.value;

    if (user) {
      user.activePlanId = planId;
    }
  }
}
