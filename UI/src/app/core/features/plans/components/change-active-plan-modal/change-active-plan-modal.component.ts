import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { PlansService } from '../../services/plans.service';
import { CommonModule } from '@angular/common';
import { CreateTravelPlanRequest, TravelPlan } from '../../models/plan.models';

@Component({
  selector: 'app-change-active-plan-modal',
  templateUrl: './change-active-plan-modal.component.html',
  styleUrls: ['./change-active-plan-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, CommonModule],
})
export class ChangeActivePlanModal implements OnInit {
  constructor(private plansService: PlansService) {}

  ngOnInit(): void {
    this.fetchPlans();
  }

  selectedPlan: TravelPlan | null = null;
  error: string = '';
  travelPlans: TravelPlan[] = [];

  @Input() isModalOpen: boolean = false;

  @Output() setActivePlanEvent = new EventEmitter<TravelPlan>();
  @Output() closeModalEvent = new EventEmitter<void>();

  formData: CreateTravelPlanRequest = {
    title: '',
    description: '',
    from: null,
    to: null,
  };

  setActivePlan(event: Event) {
    event.preventDefault();

    if (!this.selectedPlan) {
      return;
    }

    this.plansService.setActivePlan(this.selectedPlan);
    this.setActivePlanEvent.emit(this.selectedPlan);
    this.closeChangeActiveModal();
  }

  fetchPlans(): void {
    this.plansService.getPlansForUser().subscribe(
      (response) => {
        this.travelPlans = response.items;
        this.selectedPlan = response.items[0];
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
