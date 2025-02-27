import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PlansService } from './plans.service';
import { ItemListComponent } from '../../shared/item-list/item-list.components';
import { AuthService } from '../../auth/auth.service';
import { PlanCreationModal } from '../../components/plans/plan-creation-modal/plan-creation-modal.component';

interface CreateTravelPlanRequest {
  title: string;
  description: string | null;
  from: Date | null;
  to: Date | null;
}

interface TravelPlan {
  id: string;
  ownerId: string;
  participants: string[];
  title: string;
  description: string;
  from: string;
  to: string;
  additionalCostsValue: number;
  totalCostValue: number;
  planStatus: string;
}

@Component({
  selector: 'app-plans-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, ItemListComponent, PlanCreationModal],
  templateUrl: './plans-dashboard.component.html',
  styleUrls: ['./plans-dashboard.component.css'],
})
export class PlansDashboardComponent implements OnInit {
  constructor(
    private plansService: PlansService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.updatePlansForUser();
  }
  travelPlans: TravelPlan[] = [];
  activePlan: TravelPlan | null = null;

  isModalOpen: boolean = false;
  isChangeActiveModalOpen: boolean = false;
  selectedPlanId: string = '';

  error: string = '';
  formData: CreateTravelPlanRequest = {
    title: '',
    description: '',
    from: null,
    to: null,
  };

  openCreatePlanModal() {
    this.isModalOpen = true;
  }

  closeCreatePlanModal() {
    this.isModalOpen = false;
  }

  openChangeActiveModal() {
    this.isChangeActiveModalOpen = true;
  }

  closeChangeActiveModal() {
    this.isChangeActiveModalOpen = false;
  }

  setActivePlan(event: Event) {
    event.preventDefault();
    if (!this.selectedPlanId) return;

    this.plansService.setActivePlan(this.selectedPlanId).subscribe(
      () => {
        this.setUserActivePlanId(this.selectedPlanId);
        this.updatePlansForUser();
        this.closeChangeActiveModal();
      },
      (error) => {
        console.error('Error updating active plan:', error);
        this.error = 'Failed to update active plan';
      }
    );
  }

  createPlan(request: CreateTravelPlanRequest) {
    this.plansService
      .createPlan(request.title, request.description, request.from, request.to)
      .subscribe(
        (res) => {
          this.updatePlansForUser();
        },
        (error) => {
          console.log(error);
          this.error = error;
        }
      );
  }

  private updatePlansForUser() {
    this.plansService.getPlansForUser().subscribe(
      (response) => {
        this.travelPlans = response.items;
        const user = this.authService.user.value;

        if (user) {
          const activePlanId = user.activePlanId;
          this.activePlan =
            this.travelPlans.find((plan) => plan.id == activePlanId) || null;

          if (this.activePlan) {
            this.setUserActivePlanId(this.activePlan?.id);
          }
        }
      },
      (error) => {
        console.error('Error fetching plans:', error);
        this.error = 'Failed to load travel plans';
      }
    );
  }

  private setUserActivePlanId(planId: string) {
    const user = this.authService.user.value;

    if (user) {
      user.activePlanId = planId;
    }
  }
}
