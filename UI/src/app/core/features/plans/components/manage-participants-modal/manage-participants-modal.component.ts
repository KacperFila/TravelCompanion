import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { ItemListComponent } from '../../../../shared/item-list/item-list.components';
import { PlanParticipant } from '../../models/plan.models';
import { CommonModule } from '@angular/common';
import {PlansService} from "../../services/plans/plans.service";
import {UsersService} from "../../../../shared/services/users.service";
import {filter, map} from "rxjs";
import {AuthService} from "../../../../auth/auth.service";

@Component({
  selector: 'app-manage-participants-modal',
  templateUrl: './manage-participants-modal.component.html',
  styleUrls: ['./manage-participants-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, ItemListComponent, CommonModule],
})
export class ManageParticipantsModal implements OnInit {
  constructor(
    private plansService: PlansService,
    private usersService: UsersService,
    private authService: AuthService) {
  }

  currentUserId: string = '';

  allUsers: PlanParticipant[] = [];
  planParticipants: PlanParticipant[] = [];
  searchQuery: string = '';
  error: string = '';

  @Input() isModalOpen: boolean = false;
  @Input() planId: string = '';
  @Output() closeModalEvent = new EventEmitter<void>();

  ngOnInit(): void {
    this.authService.user
      .pipe(filter(user => !!user))
      .subscribe(user => {
        this.currentUserId = user!.id;
        this.fetchUsers();
      });
  }

  fetchUsers(): void {
    this.usersService.browseUsers()
      .pipe(
        map(users => users
          .filter(user => user.userId !== this.currentUserId)
          .map(user => ({
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

  addParticipant(participant: PlanParticipant) {
    this.plansService.inviteUserToPlan(this.planId, participant.id)
      .subscribe();
  }

  closeModal(): void {
    this.closeModalEvent.emit();
  }
}
