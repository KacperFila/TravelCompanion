import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PlansService } from '../../features/plans/services/plans.service';
import { ItemListComponent } from '../../shared/item-list/item-list.components';
import { AuthService } from '../../auth/auth.service';
import { PlanCreationModal } from '../../features/plans/components/plan-creation-modal/plan-creation-modal.component';
import { ChangeActivePlanModal } from '../../features/plans/components/change-active-plan-modal/change-active-plan-modal.component';
import {
  CreateTravelPlanRequest,
  TravelPlan,
} from '../../features/plans/models/plan-models';
import { UserPlansModal } from '../../features/plans/components/user-plans-modal/user-plans-modal.component';

@Component({
  selector: 'app-plans-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ItemListComponent,
    PlanCreationModal,
    ChangeActivePlanModal,
    UserPlansModal,
  ],
  templateUrl: './plans-dashboard.component.html',
  styleUrls: ['./plans-dashboard.component.css'],
})
export class PlansDashboardComponent implements OnInit {
  constructor(
    private plansService: PlansService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.onPlanUpdated();
  }

  travelPlans: TravelPlan[] = [];
  activePlan: TravelPlan | null = null;

  isCreatePlanModalOpen: boolean = false;
  isChangeActiveModalOpen: boolean = false;
  isUserPlansModalOpen: boolean = false;

  error: string = '';
  formData: CreateTravelPlanRequest = {
    title: '',
    description: '',
    from: null,
    to: null,
  };

  openCreatePlanModal() {
    this.isCreatePlanModalOpen = true;
  }

  closeCreatePlanModal() {
    this.isCreatePlanModalOpen = false;
  }

  openChangeActiveModal() {
    this.isChangeActiveModalOpen = true;
  }

  closeChangeActiveModal() {
    this.isChangeActiveModalOpen = false;
  }

  openUserPlansModal() {
    this.isUserPlansModalOpen = true;
  }

  closeUserPlansModal() {
    this.isUserPlansModalOpen = false;
  }

  updatePlansForUser(): void {
    this.plansService.getPlansForUser().subscribe(
      (res) => {
        this.travelPlans = res.items;
      },
      (error) => {
        console.error('Error fetching plans:', error);
        this.error = 'Failed to load travel plans';
      }
    );
  }

  onPlanUpdated() {
    this.activePlan = this.authService.getUserActivePlan();
  }
}
