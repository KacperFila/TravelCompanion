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
import { PlansService } from '../../services/plans.service';
import { AuthService } from '../../../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { last } from 'rxjs';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { TravelPointComponent } from "../travel-point/travel-point.component";
import { PlansSignalRService } from "../../services/plans-signalR.service";
import { UpdatedPlan } from "../../services/plans-signalR-responses.models";

@Component({
  selector: 'app-points-roadmap',
  templateUrl: './points-roadmap.component.html',
  styleUrls: ['./points-roadmap.component.css'],
  standalone: true,
  imports: [CommonModule, ModalComponent, FormsModule, TravelPointComponent],
})
export class PointsRoadmapComponent implements OnInit, OnDestroy {
  constructor(
    private plansService: PlansService,
    private plansSignalRService: PlansSignalRService,
  ) {}

  travelPlan: TravelPlan = { id: '', ownerId: '', participants: [], title: '', description: '', from: '', to: '', additionalCostsValue: 0, totalCostValue: 0, planStatus: '', planPoints: [] };
  updateRequests: Map<string, TravelPointUpdateRequest[]> = new Map<string, TravelPointUpdateRequest[]>;
  newTravelPoint: TravelPoint = { placeName: '', id: '', totalCost: 0, travelPlanOrderNumber: 0 };

  protected readonly last = last;

  @Input() planUpdated: boolean = false;
  @Input() isModalOpen: boolean = false;
  @Output() addNewPointEvent = new EventEmitter<void>();
  @Output() closeCreatePointModalEvent = new EventEmitter<void>();

  ngOnInit(): void {
    this.getActivePlan();
    this.setupSignalRListeners();
  }

  ngOnDestroy(): void {
    this.plansSignalRService.stopConnection()
  }

  getActivePlan(): void {
    this.plansService.getActivePlanWithPoints()
      .subscribe(
      (activePlan) => {
        this.travelPlan = activePlan;
        this.travelPlan.planPoints.forEach(point => {
          this.getTravelPointEditRequests(point.id);
        });
      })
  }

  createPoint(event: Event): void {
    event.preventDefault();

    if (!this.newTravelPoint?.placeName.trim())
    {
      return;
    }

    if (!this.travelPlan)
    {
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
    if (!this.travelPlan)
    {
      return;
    }

    this.plansService.deletePoint(point.id).subscribe({
      error: (err) => console.error('Error deleting point', err),
    });
  }

  closeCreatePointModal() {
    this.isModalOpen = false;
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

  private setupSignalRListeners(): void {
    this.plansSignalRService.listenForUpdates("ReceivePlanUpdate", (updatedPlan: UpdatedPlan) => {
      this.travelPlan.planPoints = updatedPlan.travelPlanPoints.map(
        (planPoint) => (
          {
          id: planPoint.id.value,
          placeName: planPoint.placeName,
          totalCost: planPoint.totalCost.amount,
          travelPlanOrderNumber: planPoint.travelPlanOrderNumber
          }
        ));
    });

    this.plansSignalRService.listenForUpdates(
      "ReceiveTravelPointUpdateRequestUpdate",
      (data: UpdateRequestUpdateResponse) => {
        this.updateRequests = new Map(this.updateRequests.set(data.pointId.value, data.updateRequests));
      });
  }
}
