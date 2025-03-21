import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../auth/auth.service';
import { PlanCreationModal } from '../../features/plans/components/plan-creation-modal/plan-creation-modal.component';
import { ChangeActivePlanModal } from '../../features/plans/components/change-active-plan-modal/change-active-plan-modal.component';
import {
  TravelPlan
} from '../../features/plans/models/plan.models';
import { UserPlansModal } from '../../features/plans/components/user-plans-modal/user-plans-modal.component';
import { PointsRoadmapComponent } from '../../features/plans/components/points-roadmap/points-roadmap.component';

@Component({
  selector: 'app-plans-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    PlanCreationModal,
    ChangeActivePlanModal,
    UserPlansModal,
    PointsRoadmapComponent,
  ],
  templateUrl: './plans-dashboard.component.html',
  styleUrls: ['./plans-dashboard.component.css'],
})
export class PlansDashboardComponent implements OnInit {
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.onPlanUpdated();
  }

  activePlan: TravelPlan | null = null;

  isCreatePlanModalOpen: boolean = false;
  isChangeActiveModalOpen: boolean = false;
  isUserPlansModalOpen: boolean = false;
  isCreatePointModalOpen: boolean = false;

  error: string = '';

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

  closeCreatePointModal() {
    this.isCreatePointModalOpen = false;
  }

  onPlanUpdated() {
    this.activePlan = this.authService.getUserActivePlan();
  }
}
