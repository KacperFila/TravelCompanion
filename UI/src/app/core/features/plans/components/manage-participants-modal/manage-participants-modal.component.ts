import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { ItemListComponent } from '../../../../shared/item-list/item-list.components';
import { PlanParticipant } from '../../models/plan.models';
import { CommonModule } from '@angular/common';
import {PlansService} from "../../services/plans/plans.service";
import {UsersService} from "../../services/users/users.service";
import {map} from "rxjs";

@Component({
  selector: 'app-manage-participants-modal',
  templateUrl: './manage-participants-modal.component.html',
  styleUrls: ['./manage-participants-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, ItemListComponent, CommonModule],
})
export class ManageParticipantsModal implements OnInit {
  constructor(private plansService: PlansService, private usersService: UsersService) {
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
    this.usersService.browseUsers()
      .pipe(
        map(users => users.map(user => ({
          id: user.userId,
          email: user.email,
        })))
      )
      .subscribe(mappedUsers => {
        this.allUsers = mappedUsers;
        this.planParticipants = [...mappedUsers];
      });
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
    this.plansService.inviteUserToPlan(this.planId, item.id)
      .subscribe();
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
