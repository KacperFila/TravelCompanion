import {Component, ViewChild} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PlanCreationModal } from '../../features/plans/components/plan-creation-modal/plan-creation-modal.component';
import { ChangeActivePlanModal } from '../../features/plans/components/change-active-plan-modal/change-active-plan-modal.component';
import { UserPlansModal } from '../../features/plans/components/user-plans-modal/user-plans-modal.component';
import { PointsRoadmapComponent } from '../../features/plans/components/points-roadmap/points-roadmap.component';
import {
  ManageParticipantsModal,
} from "../../features/plans/components/manage-participants-modal/manage-participants-modal.component";

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
    ManageParticipantsModal,
    UserPlansModal,

  ],
  templateUrl: './plans-dashboard.component.html',
  styleUrls: ['./plans-dashboard.component.css'],
})
export class PlansDashboardComponent {
  constructor() {}

  @ViewChild(PointsRoadmapComponent) pointsRoadmapComponent!: PointsRoadmapComponent;

  isCreatePlanModalOpen: boolean = false;
  isChangeActiveModalOpen: boolean = false;
  isUserPlansModalOpen: boolean = false;
  isCreatePointModalOpen: boolean = false;
  isManageParticipantsModalOpen: boolean = false;

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

  openManageParticipantsModal(){
    this.isManageParticipantsModalOpen = true;
  }

  closeManageParticipantsModal() {
    this.isManageParticipantsModalOpen = false;
  }

  activePlanChanged() {
    this.pointsRoadmapComponent.getActivePlan()
  }
}
