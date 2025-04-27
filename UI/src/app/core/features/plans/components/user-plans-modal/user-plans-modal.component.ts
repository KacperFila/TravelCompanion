import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { PlansService } from '../../services/plans/plans.service';
import { ItemListComponent } from '../../../../shared/item-list/item-list.components';
import { TravelPlan } from '../../models/plan.models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-plans-modal',
  templateUrl: './user-plans-modal.component.html',
  styleUrls: ['./user-plans-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, ItemListComponent, CommonModule],
})
export class UserPlansModal implements OnInit {
  constructor(private plansService: PlansService) {}

  travelPlans: TravelPlan[] = [];
  error: string = '';

  @Input() isModalOpen: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();

  ngOnInit(): void {
    this.fetchPlans();
  }

  fetchPlans(): void {
    this.plansService.getPlansForUser().subscribe(
      (response: TravelPlan[]) => {
        this.travelPlans = response;
      },
      (error) => {
        console.error('Error fetching travel plans:', error);
        this.error = 'Failed to load travel plans';
      }
    );
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
