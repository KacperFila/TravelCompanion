import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import {
  CreateTravelPointRequest,
  TravelPoint,
} from '../../models/plan.models';
import { PlansService } from '../../services/plans.service';
import { AuthService } from '../../../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-points-roadmap',
  templateUrl: './points-roadmap.component.html',
  styleUrls: ['./points-roadmap.component.css'],
  standalone: true,
  imports: [CommonModule, ModalComponent, FormsModule],
})
export class PointsRoadmapComponent implements OnInit, OnDestroy {
  constructor(
    private plansService: PlansService,
    private authService: AuthService
  ) {}

  travelPoints: TravelPoint[] = [];
  newTravelPoint: TravelPoint = { placeName: '', id: '', totalCost: 0 };
  private activePlanSubscription!: Subscription;

  @Input() isModalOpen: boolean = false;
  @Output() addNewPointEvent = new EventEmitter<void>();
  @Output() closeCreatePointModalEvent = new EventEmitter<void>();

  formData: CreateTravelPointRequest = {
    placeName: '',
    travelPlanId: '',
  };

  ngOnInit(): void {
    this.activePlanSubscription = this.authService.activePlan$.subscribe(
      (activePlan) => {
        if (activePlan) {
          this.fetchPoints(activePlan.id);
        } else {
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
    this.plansService.getActivePlanWithPoints(planId).subscribe((response) => {
      this.travelPoints = response.planPoints;
    });
  }

  createPoint(event: Event): void {
    event.preventDefault();

    if (!this.newTravelPoint?.placeName.trim()) return;

    const activePlanId = this.authService.getUserActivePlan()?.id;
    if (!activePlanId) return;

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

  closeCreatePointModal() {
    this.closeCreatePointModalEvent.emit();
  }
}
