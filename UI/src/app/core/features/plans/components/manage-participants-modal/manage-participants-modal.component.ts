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
  @Input() planId: string = '';
  @Output() closeModalEvent = new EventEmitter<void>();

  ngOnInit(): void {
    this.fetchUsers();
  }

  fetchUsers(): void {
    this.allUsers = [
      { id: { value: "4125fa31-5521-473c-91a3-56f68034e9c8" }, email: 'test2@test.com' },
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
    this.plansService.inviteUserToPlan(this.planId, item.id.value)
      .subscribe();
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
