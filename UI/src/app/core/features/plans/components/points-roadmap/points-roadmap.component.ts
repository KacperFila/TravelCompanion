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
import {last, Subscription} from 'rxjs';
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

  activePlanId: string | null = null;
  travelPoints: TravelPoint[] = [];
  updateRequests: Map<string, TravelPointUpdateRequest[]> = new Map<string, TravelPointUpdateRequest[]>;
  newTravelPoint: TravelPoint = { placeName: '', id: '', totalCost: 0, travelPlanOrderNumber: 0 };

  private activePlanSubscription!: Subscription;
  protected readonly last = last;

  @Input() planUpdated: boolean = false;
  @Input() isModalOpen: boolean = false;
  @Output() addNewPointEvent = new EventEmitter<void>();
  @Output() closeCreatePointModalEvent = new EventEmitter<void>();

  ngOnInit(): void {
    this.setupSignalRListeners();
    this.getActivePlan();
  }

  ngOnDestroy(): void {
    if (this.activePlanSubscription) {
      this.activePlanSubscription.unsubscribe();
    }
  }

  getActivePlan(): void {
    this.plansService.getActivePlanWithPoints().subscribe(
      (response) => {
        this.activePlanId = response.id;
        this.travelPoints = response.planPoints;
        this.travelPoints.forEach(point => {
          this.getTravelPointEditRequests(point.id);
        });

        this.authService.setActivePlan(response.id);
      })
  }

  createPoint(event: Event): void {
    event.preventDefault();

    if (!this.newTravelPoint?.placeName.trim())
    {
      return;
    }

    if (!this.activePlanId)
    {
      return;
    }

    this.plansService
      .addPointToPlan(this.activePlanId, this.newTravelPoint.placeName)
      .subscribe({
        next: () => {
          // this.fetchPoints(activePlanId);
          this.closeCreatePointModal();
        },
        error: (err) => console.error('Error creating point', err),
      });
  }

  deletePoint(point: TravelPoint) {
    if (!this.activePlanId) {
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
    this.plansService.getTravelPointEditRequests(travelPointId).subscribe({
      next: (response) => {
        this.updateRequests.set(travelPointId, response);
      },
      error: (err) => {
        console.error("Error fetching data:", err);
      },
    });
  }

  private setupSignalRListeners(): void {
    this.signalRService.listenForUpdates("ReceivePlanUpdate", (updatedRoadmap: UpdatedPlan) => {
      const user = this.authService.user;
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
        this.updateRequests = new Map(this.updateRequests.set(data.pointId.value, data.updateRequests));
      });
  }
}
