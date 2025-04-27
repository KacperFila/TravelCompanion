import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { PlansService } from '../../services/plans/plans.service';
import { CommonModule } from '@angular/common';
import { TravelPlan } from '../../models/plan.models';
import {Subscription, switchMap, tap} from "rxjs";
import {PlansSignalRService} from "../../services/plans/plans-signalR.service";

@Component({
  selector: 'app-change-active-plan-modal',
  templateUrl: './change-active-plan-modal.component.html',
  styleUrls: ['./change-active-plan-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, CommonModule],
})
export class ChangeActivePlanModal implements OnInit, OnDestroy {
  constructor(private plansService: PlansService, private plansSignalRService: PlansSignalRService) {}

  private subscriptions = new Subscription();

  ngOnInit() {
    this.fetchPlans();

    this.subscriptions.add(
      this.plansSignalRService.activePlanChanged$.subscribe(() => {
        this.fetchPlans();
      })
    );
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  selectedPlan: TravelPlan | null = null;
  error: string = '';
  travelPlans: TravelPlan[] = [];

  @Input() isModalOpen: boolean = false;
  @Output() setActivePlanEvent = new EventEmitter();
  @Output() closeModalEvent = new EventEmitter<void>();

  setActivePlan(event: Event) {
    event.preventDefault();

    if (!this.selectedPlan)
    {
      return;
    }

    this.plansService.setActivePlan(this.selectedPlan.id).pipe(
      switchMap(() => this.plansService.getActivePlanWithPoints())
    ).subscribe(response => {
      this.setActivePlanEvent.emit(response);
    });

    this.closeChangeActiveModal();
  }

  fetchPlans(): void {
    this.plansService.getActivePlanWithPoints()
      .pipe(
        tap(response => {
            this.selectedPlan = response;
      }),
      switchMap(() => this.plansService.getPlansForUser())
    ).subscribe({
      next: (response) => {
        this.travelPlans = response;
        this.selectedPlan = this.travelPlans.find(plan => plan.id === this.selectedPlan?.id) || null;
      },
      error: (error) => {
        console.error('Error fetching plans:', error);
        this.error = 'Failed to load travel plans';
      }
    });
  }

  closeChangeActiveModal(): void {
    this.closeModalEvent.emit();
  }
}
