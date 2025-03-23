import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { ItemListComponent } from '../../../../shared/item-list/item-list.components';
import { PlanParticipant } from '../../models/plan.models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-manage-participants-modal',
  templateUrl: './manage-participants-modal.component.html',
  styleUrls: ['./manage-participants-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, ItemListComponent, CommonModule],
})
export class ManageParticipantsModal implements OnInit {
  allUsers: PlanParticipant[] = []; // Full list from mock API
  planParticipants: PlanParticipant[] = []; // Filtered list for UI
  searchQuery: string = '';
  error: string = '';

  @Input() isModalOpen: boolean = false;
  @Output() closeModalEvent = new EventEmitter<void>();

  ngOnInit(): void {
    this.fetchUsers();
  }

  fetchUsers(): void {
    // Mock API Response
    this.allUsers = [
      { id: { value: '1' }, email: 'alice@example.com' },
      { id: { value: '2' }, email: 'bob@example.com' },
      { id: { value: '3' }, email: 'charlie@example.com' },
      { id: { value: '4' }, email: 'david@example.com' },
      { id: { value: '5' }, email: 'eve@example.com' },
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

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
