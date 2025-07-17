import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {Subscription, switchMap, tap} from "rxjs";
import {TravelsSignalRService} from "../../services/travels/travels-signalR.service";
import {TravelsService} from "../../services/travels/travels.service";
import {TravelDetailsDto} from "../../models/travel.models";

@Component({
  selector: 'app-change-active-travel-modal',
  templateUrl: './change-active-travel-modal.component.html',
  styleUrls: ['./change-active-travel-modal.component.css'],
  standalone: true,
  imports: [ModalComponent, FormsModule, CommonModule],
})
export class ChangeActiveTravelModal implements OnInit, OnDestroy {
  constructor(private travelsService: TravelsService, private travelsSignalRService: TravelsSignalRService) {}

  private subscriptions = new Subscription();

  ngOnInit() {
    this.fetchTravels();

    this.subscriptions.add(
      this.travelsSignalRService.activeTravelChanged$.subscribe(() => {
        this.fetchTravels();
      })
    );
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  selectedTravel: TravelDetailsDto | null = null;
  error: string = '';
  travels: TravelDetailsDto[] = [];

  @Input() isModalOpen: boolean = false;
  @Output() setActiveTravelEvent = new EventEmitter();
  @Output() closeModalEvent = new EventEmitter<void>();

  setActivePlan(event: Event) {
    event.preventDefault();

    if (!this.selectedTravel)
    {
      return;
    }

    this.travelsService.setActiveTravel(this.selectedTravel.id).pipe(
      switchMap(() => this.travelsService.getActiveTravel())
    ).subscribe(response => {
      this.setActiveTravelEvent.emit(response);
    });

    this.closeChangeActiveModal();
  }

  fetchTravels(): void {
    this.travelsService.getActiveTravel()
      .pipe(
        tap(response => {
            this.selectedTravel = response;
      }),
      switchMap(() => this.travelsService.getTravelsForUser())
    ).subscribe({
      next: (response) => {
        this.travels = response;
        this.selectedTravel = this.travels.find(travel => travel.id === this.selectedTravel?.id) || null;
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
