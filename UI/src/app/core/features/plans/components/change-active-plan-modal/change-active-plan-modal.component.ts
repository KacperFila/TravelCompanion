import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { PlansService } from '../../services/plans.service';
import { AuthService } from '../../../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { switchMap, tap } from 'rxjs';
import { CreateTravelPlanRequest, TravelPlan } from '../../models/plan-models';

@Component({
  selector: 'app-change-active-plan-modal',
  templateUrl: './change-active-plan-modal.component.html',
  styleUrls: ['./change-active-plan-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, CommonModule],
})
export class ChangeActivePlanModal implements OnInit {
  constructor(
    private plansService: PlansService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.plansService
      .getPlansForUser()
      .pipe(
        tap((response) => {
          this.travelPlans = response.items;
        }),
        switchMap(() => this.authService.user)
      )
      .subscribe(
        (user) => {
          if (user && !user.activePlan && this.travelPlans.length > 0) {
            user.activePlan = this.travelPlans[0];
          }
        },
        (error) => {
          console.error('Error fetching plans:', error);
          this.error = 'Failed to load travel plans';
        }
      );
  }

  selectedPlanId: string = '';
  error: string = '';
  travelPlans: TravelPlan[] = [];

  @Input() isModalOpen: boolean = false;

  @Output() setActivePlanEvent = new EventEmitter<CreateTravelPlanRequest>();
  @Output() closeModalEvent = new EventEmitter<void>();

  formData: CreateTravelPlanRequest = {
    title: '',
    description: '',
    from: null,
    to: null,
  };

  setActivePlan(event: Event) {
    event.preventDefault();
    if (!this.selectedPlanId) {
      return;
    }

    // this.authService.updateActivePlan(this.selectedPlanId);
    this.setActivePlanEvent.emit();
    this.closeModalEvent.emit();
  }

  closeChangeActiveModal(): void {
    this.closeModalEvent.emit();
  }
}
