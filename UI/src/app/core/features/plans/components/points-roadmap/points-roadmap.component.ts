import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import {
  TravelPlan,
  TravelPoint, TravelPointUpdateRequest, UpdateRequestUpdateResponse
} from '../../models/plan.models';
import { PlansService } from '../../services/plans/plans.service';
import { CommonModule } from '@angular/common';
import {last, Subscription} from 'rxjs';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { TravelPointComponent } from "../travel-point/travel-point.component";
import { PlansSignalRService } from "../../services/plans/plans-signalR.service";
import {
  PlanInvitationRemovedResponse,
  PlanInvitationResponse,
  UpdatedPlan
} from "../../services/plans/plans-signalR-responses.models";
import {ManageParticipantsModal} from "../manage-participants-modal/manage-participants-modal.component";
import {ChangeActivePlanModal} from "../change-active-plan-modal/change-active-plan-modal.component";
import {PlanCreationModal} from "../plan-creation-modal/plan-creation-modal.component";
import {UserPlansModal} from "../user-plans-modal/user-plans-modal.component";
import {UserInvitationsModalComponent} from "../user-invitations-modal/user-invitations-modal.component";

@Component({
  selector: 'app-points-roadmap',
  templateUrl: './points-roadmap.component.html',
  styleUrls: ['./points-roadmap.component.css'],
  standalone: true,
  imports: [CommonModule, ModalComponent, FormsModule, TravelPointComponent, ManageParticipantsModal, ChangeActivePlanModal, PlanCreationModal, UserPlansModal, UserInvitationsModalComponent],
})
export class PointsRoadmapComponent implements OnInit, OnDestroy {
  constructor(
    private plansService: PlansService,
    private plansSignalRService: PlansSignalRService,
  ) {}

  travelPlan: TravelPlan = { id: '', ownerId: '', participants: [], title: '', description: '', from: '', to: '', additionalCostsValue: 0, totalCostValue: 0, planStatus: '', travelPlanPoints: [] };
  updateRequests: Map<string, TravelPointUpdateRequest[]> = new Map<string, TravelPointUpdateRequest[]>;
  newTravelPoint: TravelPoint = { placeName: '', id: '', totalCost: 0, travelPlanOrderNumber: 0 };
  invitations: PlanInvitationResponse[] =[];
  private invitationsSubscription!: Subscription;
  private planSubscription!: Subscription;

  protected readonly last = last;

  @Input() planUpdated: boolean = false;

  isCreatePointModalOpen: boolean = false;
  isManageParticipantsModalOpen: boolean = false;
  isCreatePlanModalOpen: boolean = false;
  isChangeActiveModalOpen: boolean = false;
  isUserPlansModalOpen: boolean = false;
  isUserInvitationsModalOpen: boolean = false;

  @Output() addNewPointEvent = new EventEmitter<void>();
  @Output() closeCreatePointModalEvent = new EventEmitter<void>();

  ngOnInit(): void {
    this.invitationsSubscription = this.plansSignalRService.invitations$
      .subscribe((invitations) => {
      this.invitations = invitations;
    });

    this.planSubscription = this.plansSignalRService.travelPlan$
      .subscribe((updatedPlan) => {
      if (updatedPlan) {
        this.travelPlan = updatedPlan;
        this.travelPlan.travelPlanPoints.forEach((point) => {
          this.getTravelPointEditRequests(point.id);
        });
      }
    });
  }

  ngOnDestroy(): void {
    if (this.invitationsSubscription) {
      this.invitationsSubscription.unsubscribe();
    }
  }

  createPoint(event: Event): void {
    event.preventDefault();

    if (!this.newTravelPoint?.placeName.trim())    {
      return;
    }

    if (!this.travelPlan){
      return;
    }

    this.plansService
      .addPointToPlan(this.travelPlan.id, this.newTravelPoint.placeName)
      .subscribe({
        next: () => {
          this.closeCreatePointModal();
        },
        error: (err) => console.error('Error creating point', err),
      });
  }

  deletePoint(point: TravelPoint) {
    if (!this.travelPlan){
      return;
    }

    this.plansService.deletePoint(point.id).subscribe({
      error: (err) => console.error('Error deleting point', err),
    });
  }

  getTravelPointEditRequests(travelPointId: string) {
    this.plansService.getTravelPointEditRequests(travelPointId)
      .subscribe({
        next: (response) => {
          this.updateRequests.set(travelPointId, response);
        },
        error: (err) => {
          console.error("Error fetching data:", err);
        },
      });
  }
  closeCreatePointModal(){
    this.isCreatePointModalOpen = false;
  }

  closeManageParticipantsModal(){
    this.isManageParticipantsModalOpen = false;
  }

  closeCreatePlanModal(){
    this.isCreatePlanModalOpen = false;
  }

  closeChangeActiveModal(){
    this.isChangeActiveModalOpen = false;
  }

  closeUserPlansModal(){
    this.isUserPlansModalOpen = false;
  }

  closeUserInvitationsModal(){
    this.isUserInvitationsModalOpen = false;
  }
}
