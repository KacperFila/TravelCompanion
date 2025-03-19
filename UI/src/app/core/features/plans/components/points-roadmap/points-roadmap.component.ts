import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import {
  TravelPoint, TravelPointUpdateRequest, UpdateRequestUpdateResponse
} from '../../models/plan.models';
import { PlansService } from '../../services/plans.service';
import { AuthService } from '../../../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { last, Subscription } from 'rxjs';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { TravelPointComponent } from "../travel-point/travel-point.component";
import { SignalRService } from "../../../../shared/services/signalr.service";
import { UpdatedPlan } from "../../../../shared/services/signalr-responses.models";

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
    private authService: AuthService,
    private signalRService: SignalRService,
  ) {}

  travelPoints: TravelPoint[] = [];
  updateRequests: Map<string, TravelPointUpdateRequest[]> = new Map<string, TravelPointUpdateRequest[]>;
  newTravelPoint: TravelPoint = { placeName: '', id: '', totalCost: 0, travelPlanOrderNumber: 0 };

  private activePlanSubscription!: Subscription;

  @Input() isModalOpen: boolean = false;
  @Output() addNewPointEvent = new EventEmitter<void>();
  @Output() closeCreatePointModalEvent = new EventEmitter<void>();

  ngOnInit(): void {
    this.setupSignalRListeners();
    this.activePlanSubscription = this.authService.activePlan$.subscribe(
      (activePlan) => {
        if (activePlan)
        {
          this.fetchPoints(activePlan.id);
        }
        else
        {
          this.travelPoints = [];
        }
      }
    );
  }

  ngOnDestroy(): void {
    if (this.activePlanSubscription) {
      this.activePlanSubscription.unsubscribe();
    }
  }

  fetchPoints(planId: string): void {
    this.plansService.getActivePlanWithPoints(planId)
      .subscribe((response) => {
      this.travelPoints = response.planPoints;
        this.travelPoints.forEach(point => {
          this.getTravelPointEditRequests(point.id);
        });
    });
  }

  createPoint(event: Event): void {
    event.preventDefault();

    if (!this.newTravelPoint?.placeName.trim()) return;

    const activePlanId = this.authService.getUserActivePlan()?.id;
    if (!activePlanId)
    {
      return;
    }

    this.plansService
      .addPointToPlan(activePlanId, this.newTravelPoint.placeName)
      .subscribe({
        next: () => {
          this.fetchPoints(activePlanId);
          this.closeCreatePointModal();
        },
        error: (err) => console.error('Error creating point', err),
      });
  }

  deletePoint(point: TravelPoint) {
    const activePlanId = this.authService.getUserActivePlan()?.id;
    if (!activePlanId) return;

    this.plansService.deletePoint(point.id).subscribe({
      next: () => {
        this.fetchPoints(activePlanId);
      },
      error: (err) => console.error('Error deleting point', err),
    });
  }

  closeCreatePointModal() {
    this.isModalOpen = false;
  }

  getTravelPointEditRequests(travelPointId: string) {
    this.plansService.getTravelPointEditRequests(travelPointId).subscribe({
      next: (response) => {
        this.updateRequests.set(travelPointId, response);
      },
      error: (err) => {
        console.error("Error fetching data:", err);
      },
    });
  }

  protected readonly last = last;

  private setupSignalRListeners(): void {
    this.signalRService.listenForUpdates("ReceivePlanUpdate", (updatedRoadmap: UpdatedPlan) => {
      this.travelPoints = updatedRoadmap.travelPlanPoints.map(
        (planPoint) => ({
          id: planPoint.id.value,
          placeName: planPoint.placeName,
          totalCost: planPoint.totalCost.amount,
          travelPlanOrderNumber: planPoint.travelPlanOrderNumber
        }));
    });

    this.signalRService.listenForUpdates(
      "ReceiveTravelPointUpdateRequestUpdate",
      (data: UpdateRequestUpdateResponse) => {
        this.updateRequests.set(data.pointId, data.updateRequests);
      });
  }
}
