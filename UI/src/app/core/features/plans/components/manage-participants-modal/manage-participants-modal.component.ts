import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { ItemListComponent } from '../../../../shared/item-list/item-list.components';
import { PlanParticipant } from '../../models/plan.models';
import { CommonModule } from '@angular/common';
import {PlansService} from "../../services/plans.service";

@Component({
  selector: 'app-manage-participants-modal',
  templateUrl: './manage-participants-modal.component.html',
  styleUrls: ['./manage-participants-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, ItemListComponent, CommonModule],
})
export class ManageParticipantsModal implements OnInit {
  constructor(private plansService: PlansService) {
  }

  allUsers: PlanParticipant[] = [];
  planParticipants: PlanParticipant[] = [];
  searchQuery: string = '';
  error: string = '';

  @Input() isModalOpen: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();

  ngOnInit(): void {
    this.fetchUsers();
  }

  fetchUsers(): void {
    this.allUsers = [
      { id: { value: "f6f210c2-2e0c-48c3-8d2b-c980fa50c0e5" }, email: 'test2@test.com' },
    ];
    this.planParticipants = [...this.allUsers];
  }

  filterUsers(): void {
    if (!this.searchQuery.trim()) {
      this.planParticipants = [...this.allUsers];
      return;
    }

    const query = this.searchQuery.toLowerCase();
    this.planParticipants = this.allUsers.filter(user =>
      user.email.toLowerCase().includes(query)
    );
  }

  addParticipant(item: PlanParticipant) {
    console.log('Adding participant:', item);
    this.plansService.inviteUserToPlan("6e5fd955-e0fd-42b7-9b3f-bc733ac34e2d", item.id.value)
      .subscribe();
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
