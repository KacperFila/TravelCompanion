import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import {
  TravelPlan,
  TravelPoint,
  TravelPointUpdateRequest,
} from '../../models/plan.models';
import { PlansService } from '../../services/plans/plans.service';
import { PlansSignalRService } from '../../services/plans/plans-signalR.service';
import { PlanInvitationResponse } from '../../services/plans/plans-signalR-responses.models';
import { PlanPointComponent } from '../plan-point/plan-point.component';
import { ManageParticipantsModal } from '../manage-participants-modal/manage-participants-modal.component';
import { ChangeActivePlanModal } from '../change-active-plan-modal/change-active-plan-modal.component';
import { PlanCreationModal } from '../plan-creation-modal/plan-creation-modal.component';
import { UserPlansModal } from '../user-plans-modal/user-plans-modal.component';
import { UserInvitationsModalComponent } from '../user-invitations-modal/user-invitations-modal.component';
import { CreatePlanPointModal } from '../create-plan-point-modal/create-plan-point-modal.component';
import { EditRequestModalComponent } from '../edit-request-modal/edit-request-modal.component';
import { EditPointModalComponent } from '../edit-point-modal/edit-point-modal.component';
import {AcceptPlanModalComponent} from "../accept-plan-modal/accept-plan-modal.component";

@Component({
  selector: 'app-plan-roadmap',
  templateUrl: './plan-roadmap.component.html',
  styleUrls: ['./plan-roadmap.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    PlanPointComponent,
    ManageParticipantsModal,
    ChangeActivePlanModal,
    PlanCreationModal,
    UserPlansModal,
    UserInvitationsModalComponent,
    CreatePlanPointModal,
    EditRequestModalComponent,
    EditPointModalComponent,
    AcceptPlanModalComponent
  ],
})
export class PlanRoadmapComponent implements OnInit, OnDestroy {
  @Input() planUpdated = false;
  @Output() addNewPointEvent = new EventEmitter<void>();
  @Output() closeCreatePointModalEvent = new EventEmitter<void>();

  travelPlan: TravelPlan = {} as TravelPlan;
  invitations: PlanInvitationResponse[] = [];
  pointUpdateRequestsMap = new Map<string, TravelPointUpdateRequest[]>();
  selectedUpdateRequests: TravelPointUpdateRequest[] = [];
  pointToEdit!: TravelPoint;

  isCreatePointModalOpen = false;
  isManageParticipantsModalOpen = false;
  isCreatePlanModalOpen = false;
  isChangeActiveModalOpen = false;
  isUserPlansModalOpen = false;
  isUserInvitationsModalOpen = false;
  isAcceptPlanModalOpen = false;
  isEditRequestsModalOpen = false;
  isEditPointModalOpen = false;

  private invitationsSubscription!: Subscription;
  private planSubscription!: Subscription;
  private requestsSubscription!: Subscription;

  constructor(
    private plansService: PlansService,
    private plansSignalRService: PlansSignalRService
  ) {}

  ngOnInit(): void {
    this.invitationsSubscription = this.plansSignalRService.invitations$.subscribe(
      (invitations) => this.onInvitations(invitations)
    );

    this.planSubscription = this.plansSignalRService.travelPlan$.subscribe(
      (plan) => this.onPlanUpdate(plan)
    );

    this.requestsSubscription = this.plansSignalRService.updateRequests$.subscribe(
      (event) => this.onRequestsUpdate(event)
    );
  }

  ngOnDestroy(): void {
    this.invitationsSubscription?.unsubscribe();
    this.planSubscription?.unsubscribe();
    this.requestsSubscription?.unsubscribe();
  }

  onShowEditRequests(requests: TravelPointUpdateRequest[]): void {
    this.selectedUpdateRequests = requests;
    this.isEditRequestsModalOpen = true;
  }

  onShowEditPoint(point: TravelPoint): void {
    this.pointToEdit = { ...point };
    this.isEditPointModalOpen = true;
  }

  onEditedPoint(point: TravelPoint): void {
    this.plansService.updatePoint(point).subscribe();
    this.isEditPointModalOpen = false;
  }

  onTravelPointCreated(newPoint: TravelPoint): void {
    if (!newPoint?.placeName.trim()) {
      return;
    }

    this.plansService.addPointToPlan(this.travelPlan.id, newPoint.placeName).subscribe({
      next: () => this.closeCreatePointModal(),
      error: (err) => console.error('Error creating point', err),
    });

    this.isCreatePointModalOpen = false;
  }

  deletePoint(point: TravelPoint): void {
    this.plansService.deletePoint(point.id).subscribe({
      error: (err) => console.error('Error deleting point', err),
    });
  }

  closeCreatePointModal(): void {
    this.isCreatePointModalOpen = false;
  }

  closeManageParticipantsModal(): void {
    this.isManageParticipantsModalOpen = false;
  }

  closeCreatePlanModal(): void {
    this.isCreatePlanModalOpen = false;
  }

  closeChangeActiveModal(): void {
    this.isChangeActiveModalOpen = false;
  }

  closeUserPlansModal(): void {
    this.isUserPlansModalOpen = false;
  }

  closeUserInvitationsModal(): void {
    this.isUserInvitationsModalOpen = false;
  }

  closeAcceptPlanModal(): void {
    this.isAcceptPlanModalOpen = false;
  }

  private onInvitations(invitations: PlanInvitationResponse[]): void {
    this.invitations = invitations;
  }

  private onPlanUpdate(plan: TravelPlan | null): void {
    if (plan) {
      this.travelPlan = plan;
    }
  }

  private onRequestsUpdate(
    event: { updateRequests: TravelPointUpdateRequest[] } | null
  ): void {
    const requestsGroupedByPoint: Record<string, TravelPointUpdateRequest[]> = {};

    for (const request of event?.updateRequests ?? []) {
      const pointId = request.travelPlanPointId;
      requestsGroupedByPoint[pointId] ??= [];
      requestsGroupedByPoint[pointId].push(request);
    }

    for (const [pointId, requests] of Object.entries(requestsGroupedByPoint)) {
      this.pointUpdateRequestsMap.set(pointId, requests);
    }

    for (const pointId of this.pointUpdateRequestsMap.keys()) {
      if (!(pointId in requestsGroupedByPoint)) {
        this.pointUpdateRequestsMap.delete(pointId);
      }
    }
  }
}
