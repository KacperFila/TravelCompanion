import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { PlansService } from '../../services/plans.service';
import { CommonModule } from '@angular/common';
import { CreateTravelPlanRequest, TravelPlan } from '../../models/plan.models';
import {AuthService} from "../../../../auth/auth.service";
import {switchMap} from "rxjs";

@Component({
  selector: 'app-change-active-plan-modal',
  templateUrl: './change-active-plan-modal.component.html',
  styleUrls: ['./change-active-plan-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, CommonModule],
})
export class ChangeActivePlanModal implements OnInit {
  constructor(private plansService: PlansService, private authService: AuthService ) {}

  ngOnInit(): void {
    this.fetchPlans();
  }

  selectedPlan: TravelPlan | null = null;
  error: string = '';
  travelPlans: TravelPlan[] = [];

  @Input() isModalOpen: boolean = false;
  @Output() setActivePlanEvent = new EventEmitter();
  @Output() closeModalEvent = new EventEmitter<void>();

  setActivePlan(event: Event) {
    event.preventDefault();
    if (!this.selectedPlan) return;

    this.plansService.setActivePlan(this.selectedPlan.id)
      .pipe(
      switchMap(
        () => this.plansService.getActivePlanWithPoints()
      )
    ).subscribe(response => {
      this.setActivePlanEvent.emit(response);
    });

    this.closeChangeActiveModal();
  }

  fetchPlans(): void {
      this.plansService.getPlansForUser().subscribe(
      (response) => {
        this.travelPlans = response.items;

        const activePlanId = this.authService.user.value?.activePlanId || null;
        const hasActivePlan = response.items.some(x => x.id === activePlanId);

        if (hasActivePlan)
        {
          this.selectedPlan = response.items.find(x => x.id === activePlanId) || null;
        }
      },
      (error) => {
        console.error('Error fetching plans:', error);
        this.error = 'Failed to load travel plans';
      }
    );
  }

  closeChangeActiveModal(): void {
    this.closeModalEvent.emit();
  }
}
